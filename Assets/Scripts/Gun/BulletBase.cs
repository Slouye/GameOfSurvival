using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour {
    private Transform m_Transform;
    private Rigidbody m_Rigidbody;
    

    private int damage;

    public Transform M_Transform { get { return m_Transform; } }
    public Rigidbody M_Rigidbody { get { return m_Rigidbody; } }

    public int Damage { get { return damage; } set { damage = value; } }
    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter(collision);
    }

    public IEnumerator TailAnimation(Transform Pivot)
    {
        //停止时间
        float shopTime = Time.time + 1.0f;

        //动画颤动的范围
        float range = 1.0f;

        float vel = 0;

        //颤动起始位置
        Quaternion startRot = Quaternion.Euler(new Vector3(Random.Range(5.0f, -5.0f), Random.Range(5.0f, -5.0f), 0));

        while (Time.time < shopTime)
        {
            Pivot.localRotation = Quaternion.Euler(new Vector3(Random.Range(range, -range), Random.Range(range, -range), 0));

            //平滑阻尼（动画的运动范围,动画的最终位置,这个数据我们保持为0 即可,动画时长）
            //平滑缓冲，东西不是僵硬的移动而是做减速缓冲运动到指定位置
            range = Mathf.SmoothDamp(range, 0, ref vel, shopTime - Time.time);

            yield return null;
        }
    }

    public void KillSelf()
    {
        GameObject.Destroy(gameObject);
    }

    public abstract void Init();
    public abstract void Shoot(Vector3 dir, int force, int Damage, RaycastHit hit);
    public abstract void CollisionEnter(Collision collision);

}
