using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BulletBase {

    private BoxCollider m_BoxCollider;

    private Transform m_Pivot;

    private RaycastHit hit;
    

    public override void Init()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        m_Pivot = M_Transform.Find("Pivot");
    }

    public override void Shoot(Vector3 dir, int force, int Damage, RaycastHit hit)
    {
        M_Rigidbody.AddForce(dir * force);
        this.Damage = Damage;
        this.hit = hit;
    }

    public override void CollisionEnter(Collision collision)
    {
        M_Rigidbody.Sleep();
        
        //触碰到环境
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            GameObject.Destroy(M_Rigidbody);
            GameObject.Destroy(m_BoxCollider);
            collision.collider.GetComponent<BulletMark>().Hp -= Damage;
            M_Transform.parent = collision.collider.transform;
            StartCoroutine(TailAnimation(m_Pivot));
        }

        //触碰到AI角色
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("AI"))
        {
            GameObject.Destroy(M_Rigidbody);
            GameObject.Destroy(m_BoxCollider);

            //GetComponentInParent<T>()  获取父物体的组件
            if (collision.collider.gameObject.name == "Head")
            {
                collision.collider.GetComponentInParent<AI>().HardHit(Damage*2);
            }
            else
            {
                collision.collider.GetComponentInParent<AI>().NormalHit(Damage);
            }

            collision.collider.GetComponentInParent<AI>().PlayEffect(hit);
            M_Transform.parent = collision.collider.transform;
            StartCoroutine(TailAnimation(m_Pivot));
        }
    }
}
