using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunControllerBase : MonoBehaviour {
    private GunViewBase m_GunViewBase;
    [SerializeField] private int id;                         //ID
    [SerializeField] private int damage;                     //伤害值
    [SerializeField] private int durable;                    //耐久值
    private float durable_max;
    [SerializeField] private GunType gunWeaponType;          //类型
    private AudioClip audio;                //音效资源
    private GameObject effect;              //特效资源
    Ray ray;
    RaycastHit hit;

    private bool canShoot = true;   //是否可以开枪

    private GameObject icon;        //图标ui

    public GameObject Icon { get { return icon; } set { icon = value; } }

    public int Id { get { return id; } set { id = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public GunType GunWeaponType { get { return gunWeaponType; } set { gunWeaponType = value; } }
    public int Durable
    {   get {return durable; }
        set
        {  durable = value;
            if (durable <= 0)
            {
                GameObject.Destroy(gameObject);
                GameObject.Destroy(m_GunViewBase.GunStar.gameObject);
            }
        }
    }
    public AudioClip Audio { get { return audio; } set { audio = value; } }

    public GameObject Effect { get { return effect; } set { effect = value; } }

    public GunViewBase M_GunViewBase { get { return m_GunViewBase; } set { m_GunViewBase = value; } }

    public Ray MyRay { get { return ray; } set { ray = value; } }

    public RaycastHit Hit { get { return hit; } set { hit = value; } }

    protected virtual void Start()
    {
        durable_max = durable;
        m_GunViewBase = gameObject.GetComponent<GunViewBase>();
        LoadAudioAssets();
        Init();
    }

    void Update()
    {
        ShootReady();
        MouseControl();
    }


    /// <summary>
    /// 鼠标控制
    /// </summary>
    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)            //点击鼠标左键，发射子弹
        {
            MouseButtonLeftDown();
        }

        if (Input.GetMouseButton(1))                //按住鼠标右键，开镜
        {
            MouseButtonRight();
        }
        if (Input.GetMouseButtonUp(1))              //松开右键，关镜
        {
            MouseButtonRightUp();
        }
    }

    protected void CanShoot(int state)
    {
        if (state == 0)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }
    }

    protected virtual void MouseButtonLeftDown()
    {
        Shoot();
        BarUpDate();
        PlayAudio();
        M_GunViewBase.M_Animator.SetTrigger("Fire");
    }

    //更新枪械耐久ui条
    private void BarUpDate()
    {
        icon.GetComponent<InventroyItemController>().BarUIUpDate(durable / durable_max);

        Debug.Log(durable / durable_max);
    }

    private void MouseButtonRight()
    {
        M_GunViewBase.M_Animator.SetBool("HoldPose", true);
        M_GunViewBase.EnterHoldPose();
        M_GunViewBase.GunStar.gameObject.SetActive(false);
    }

    private void MouseButtonRightUp()
    {
        M_GunViewBase.M_Animator.SetBool("HoldPose", false);
        M_GunViewBase.ExitHoldPose();
        M_GunViewBase.GunStar.gameObject.SetActive(true);
    }

    //枪械放下动作
    public void Holster()
    {
        M_GunViewBase.M_Animator.SetTrigger("Holster");
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    protected void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(Audio, M_GunViewBase.GunPoint.position);
    }

    /// <summary>
    /// 射击准备
    /// </summary>
    protected void ShootReady()
    {
        //射线（起始位置，方向）
        ray = new Ray(M_GunViewBase.GunPoint.position, M_GunViewBase.GunPoint.forward);
        //绘制射线
        //Debug.DrawLine(m_AssaultRifleView.GunPoint.position, m_AssaultRifleView.GunPoint.forward * 500, Color.green);
        if (Physics.Raycast(ray, out hit))
        {
            //设置准星位置
            Vector2 uiPos = RectTransformUtility.WorldToScreenPoint(M_GunViewBase.M_EnvCamera, Hit.point);
            M_GunViewBase.GunStar.position = uiPos;
            Debug.Log("有碰撞信息");
        }
        else
        {
            hit.point = Vector3.zero;
            Debug.Log("无碰撞信息");
        }
    }

    /// <summary>
    /// （对象池）协创延迟
    /// </summary>
    protected IEnumerator Delay(ObjectPool pool, GameObject go, float tiem)
    {
        yield return new WaitForSeconds(tiem);
        pool.AddObject(go);
    }

    protected abstract void Init();
    protected abstract void LoadAudioAssets();

    protected abstract void Shoot();
    
    
}
