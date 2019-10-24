using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ObjectPool))]
public class BulletMark : MonoBehaviour {
    private ObjectPool pool;

    private Texture2D m_BulletMark;     //弹痕贴图
    private Texture2D m_MinTexture;     //模型主贴图
    private Texture2D m_MinTextureBackup_1;     //备份模型主贴图
    private Texture2D m_MinTextureBackup_2;     //备份模型主贴图

    [SerializeField]
    private MaterialType materialType;      //材质类型

    private GameObject prefab_Effect;       //材质特效

    private Transform effectParent;

    Queue<Vector2> bulletMarkQueue;

    [SerializeField]private int hp;

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
            if (hp <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    void Start () {
       
        switch (materialType)
        {
            case MaterialType.Metal:
                ResourcesLoad("Bullet Decal_Metal", "Bullet Impact FX_Metal", "Effect_Metal_Parent");
                break;
            case MaterialType.Stone:
                ResourcesLoad("Bullet Decal_Stone", "Bullet Impact FX_Stone", "Effect_Stone_Parent");
                break;
            case MaterialType.Wood:
                ResourcesLoad("Bullet Decal_Wood", "Bullet Impact FX_Wood", "Effect_Wood_Parent");
                break;
        }

        if (gameObject.GetComponent<ObjectPool>() == null)
        {
            pool = gameObject.AddComponent<ObjectPool>();
        }
        else
        {
            pool = gameObject.GetComponent<ObjectPool>();
        }

        if (gameObject.name == "Tree")
        {
            m_MinTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().materials[2].mainTexture;
        }
        else
        {
            m_MinTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
        }

        m_MinTextureBackup_1 = GameObject.Instantiate<Texture2D>(m_MinTexture);
        m_MinTextureBackup_2 = GameObject.Instantiate<Texture2D>(m_MinTexture);
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = m_MinTextureBackup_1;
        bulletMarkQueue = new Queue<Vector2>();
    }

    /// <summary>
    /// 资源加载
    /// </summary>
    private void ResourcesLoad(string bullet,string effect,string parent)
    {
        m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/" + bullet);
        prefab_Effect = Resources.Load<GameObject>("Effects/Gun/" + effect);
        effectParent = GameObject.Find("TempObject/" + parent).GetComponent<Transform>();
    }

    /// <summary>
    /// 生成弹痕
    /// </summary>
    /// <param name="hit"></param>
    public void CreateBulletMark(RaycastHit hit)
    {
        PlayAudios(hit);

        //获取碰撞点在主贴图上的UV 坐标；
        Vector2 uv = hit.textureCoord;
        bulletMarkQueue.Enqueue(uv);
        //痕贴图的宽
        for (int i = 0; i < m_BulletMark.width; i++)
        {
            //痕贴图的高
            for (int j = 0; j < m_BulletMark.height; j++)
            {
                //uv.x* 主贴图宽度-弹痕贴图宽度 / 2 + i;
                float x = uv.x * m_MinTextureBackup_1.width - m_BulletMark.width / 2 + i;

                //uv.y* 主贴图高度-弹痕贴图高度 / 2 + j;
                float y = uv.y * m_MinTextureBackup_1.height - m_BulletMark.height / 2 + j;

                //通过循环索引获取弹痕像素点的颜色值；
                Color color = m_BulletMark.GetPixel(i, j);
                if (color.a >= 0.2f)
                {
                    //在主贴图的相应位置设置新的像素值；
                    m_MinTextureBackup_1.SetPixel((int)x, (int)y, color);
                }
                
            }
        }
        m_MinTextureBackup_1.Apply();

        //播放特效
        PlayEffect(hit);

        //5秒后消除弹痕
        Invoke("ResetBulletMark",5);
    }

    //消除弹痕
    private void ResetBulletMark()
    {
        if (bulletMarkQueue.Count > 0)
        {
            Vector2 uv = bulletMarkQueue.Dequeue();
            for (int i = 0; i < m_BulletMark.width; i++)
            {
                for (int j = 0; j < m_BulletMark.height; j++)
                {
                    float x = uv.x * m_MinTextureBackup_2.width - m_BulletMark.width / 2 + i;
                    float y = uv.y * m_MinTextureBackup_2.height - m_BulletMark.height / 2 + j;
                    Color color = m_MinTextureBackup_2.GetPixel((int)x, (int)y);
                    m_MinTextureBackup_1.SetPixel((int)x, (int)y, color);
                }
            }
            m_MinTextureBackup_1.Apply();
        }
    }

    /// <summary>
    /// 播放特效
    /// </summary>
    private void PlayEffect(RaycastHit hit)
    {
        GameObject go = null;
        if (pool.Data())
        {
            //对象池有对象
            go = pool.GetObject();
            go.GetComponent<Transform>().position = hit.point;
            go.GetComponent<Transform>().rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            //对象池没对象
            go = GameObject.Instantiate<GameObject>(prefab_Effect, hit.point, Quaternion.LookRotation(hit.normal), effectParent);
            go.name = "Effector" + materialType;
        }
        StartCoroutine(Delay(go, 1));
    }

    private IEnumerator Delay(GameObject go,float time)
    {
        yield return new WaitForSeconds(time);
        pool.AddObject(go);
    }

    /// <summary>
    /// 播放三类音效
    /// </summary>
    private void PlayAudios(RaycastHit hit)
    {
        switch (materialType)
        {
            case MaterialType.Metal:
                AudioManager.Instence.PlayAudioByName(ClipName.BulletImpactMetal, hit.point);
                break;
            case MaterialType.Stone:
                AudioManager.Instence.PlayAudioByName(ClipName.BulletImpactStone, hit.point);
                break;
            case MaterialType.Wood:
                AudioManager.Instence.PlayAudioByName(ClipName.BulletImpactWood, hit.point);
                break;
            default:
                break;
        }
    }
}
