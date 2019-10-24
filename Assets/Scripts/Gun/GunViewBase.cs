using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class GunViewBase : MonoBehaviour {
    private Transform m_Transform;
    private Animator m_Animator;
    private Camera m_EnvCamera;

    //开镜动画位置优化
    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 endPos;
    private Vector3 endRot;

    private Transform gunStar;          //准星
    private Transform gunPoint;         //枪口位置

    private GameObject prefab_GunStar;  //准星预制体

    public Transform M_Transform { get { return m_Transform; } }
    public Animator M_Animator { get { return m_Animator; } }
    public Camera M_EnvCamera { get { return m_EnvCamera; } }

    public Vector3 StartPos { get { return startPos; } set { startPos = value; } }
    public Vector3 StartRot { get { return startRot; } set { startRot = value; } }
    public Vector3 EndPos { get { return endPos; } set { endPos = value; } }
    public Vector3 EndRot { get { return endRot; } set { endRot = value; } }

    public Transform GunStar { get { return gunStar; } }
    public Transform GunPoint { get { return gunPoint; } set { gunPoint = value; } }

    protected virtual void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
         m_Animator = gameObject.GetComponent<Animator>();
        m_EnvCamera = GameObject.Find("EnvCamera").GetComponent<Camera>();
        prefab_GunStar = Resources.Load<GameObject>("Gun/GunStar");
        gunStar = GameObject.Instantiate<GameObject>(prefab_GunStar, GameObject.Find("Canvas/MainPanel").transform).transform;
        InitHoldPoseValue();
        FindGunPoint();
        Init();
    }


    //激活时调用
    private void OnEnable()
    {
        ShowStar();
    }

    //不激活时调用
    private void OnDisable()
    {
        HideStar();
    }

    private void ShowStar()
    {
        gunStar.gameObject.SetActive(true);
    }

    private void HideStar()
    {
        if (gunStar != null)
        {
            gunStar.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 进入开镜状态-->动作优化
    /// </summary>
    public void EnterHoldPose(float time = 0.2f,float fov = 40)
    {
        m_Transform.DOLocalMove(endPos, time);
        m_Transform.DOLocalRotate(endRot, time);
        m_EnvCamera.DOFieldOfView(fov, time);
    }

    /// <summary>
    /// 退出开镜状态-->动作优化
    /// </summary>
    public void ExitHoldPose(float time = 0.2f, float fov = 60)
    {
        M_Transform.DOLocalMove(StartPos, time);
        M_Transform.DOLocalRotate(StartRot, time);
        M_EnvCamera.DOFieldOfView(fov, time);
    }

    protected abstract void Init();
    /// <summary>
    /// 开镜关镜动作4个字段设置
    /// </summary>
    protected abstract void InitHoldPoseValue();

    /// <summary>
    /// 查找枪口
    /// </summary>
    protected abstract void FindGunPoint();

    
}
