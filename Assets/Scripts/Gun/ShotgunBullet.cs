using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : BulletBase {
    private Ray ray;
    private RaycastHit hit;

    private int damage;
    
    public override void Init()
    {
        Invoke("KillSelf", 3);
    }

    public override void Shoot(Vector3 dir, int force, int Damage , RaycastHit hit)
    {
        M_Rigidbody.AddForce(dir * force);
        damage = Damage;
        ray = new Ray(M_Transform.position, dir);
        //1000:射线检测多远，1<<11只检测11那个层
        //if (Physics.Raycast(ray, out hit, 1000, 1 << 11)) { }
    }

    public override void CollisionEnter(Collision collision)
    {
        M_Rigidbody.Sleep();
        if (collision.collider.GetComponent<BulletMark>() != null)
        {
            if (Physics.Raycast(ray, out hit, 1000, 1 << 11)) { }
            collision.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
            collision.collider.GetComponent<BulletMark>().Hp -= damage;
        }

        if (collision.collider.GetComponentInParent<AI>() != null)
        {
            if (Physics.Raycast(ray, out hit, 1000, 1 << 12)) { }
            collision.collider.GetComponentInParent<AI>().PlayEffect(hit);
           //血量削减
            if (collision.collider.gameObject.name == "Head")
            {
                collision.collider.GetComponentInParent<AI>().HardHit(Damage * 2);
            }
            else
            {
                collision.collider.GetComponentInParent<AI>().NormalHit(Damage);
            }
        }

        GameObject.Destroy(gameObject);
    }
}
