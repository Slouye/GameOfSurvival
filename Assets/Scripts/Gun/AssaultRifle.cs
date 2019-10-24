using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AssaultRifle : GunWeaponBase {
    private AssaultRifleView m_AssaultRifleView;
  
   
   

    private ObjectPool[] pools;
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void Init()
    {
        m_AssaultRifleView = (AssaultRifleView)M_GunViewBase;
        pools = gameObject.GetComponents<ObjectPool>();
    }




    /// <summary>
    /// 播放特效
    /// </summary>
    protected override void PlayEffect()
    {
        //枪火特效
        GameObject gunEffect = null;
        if (pools[0].Data())
        {
            gunEffect = pools[0].GetObject();
            gunEffect.GetComponent<Transform>().position = m_AssaultRifleView.GunPoint.position;
        }
        else
        {
            gunEffect = GameObject.Instantiate<GameObject>(Effect, m_AssaultRifleView.GunPoint.position, Quaternion.identity, m_AssaultRifleView.M_effectParent);
            gunEffect.name = "Effect";
        }
        gunEffect.GetComponent<ParticleSystem>().Play();
        StartCoroutine(Delay(pools[0], gunEffect,1));

        //弹壳特效
        GameObject shell = null;
        if (pools[1].Data())
        {
            shell = pools[1].GetObject();
            shell.GetComponent<Rigidbody>().isKinematic = true;
            shell.GetComponent<Transform>().position = m_AssaultRifleView.EffectPos.position;
            shell.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            shell = GameObject.Instantiate<GameObject>(m_AssaultRifleView.Shell, m_AssaultRifleView.EffectPos.position, Quaternion.identity, m_AssaultRifleView.M_shellParent);
            shell.name = "shell";
        }
        shell.GetComponent<Rigidbody>().AddForce(m_AssaultRifleView.EffectPos.up * 50);
        StartCoroutine(Delay(pools[1], shell, 3));
    }





    /// <summary>
    /// 射击
    /// </summary>
    protected override void Shoot()
    {
        if (Hit.point != Vector3.zero)
        {
            if (Hit.collider.GetComponent<BulletMark>() != null)
            {
                Hit.collider.GetComponent<BulletMark>().CreateBulletMark(Hit);
                Hit.collider.GetComponent<BulletMark>().Hp -= Damage;
            }
            else if (Hit.collider.GetComponentInParent<AI>() != null)
            {
                if (Hit.collider.gameObject.name == "Head")
                {
                    Hit.collider.GetComponentInParent<AI>().HardHit(Damage * 2);
                }
                else
                {
                    Hit.collider.GetComponentInParent<AI>().NormalHit(Damage);
                }
                Hit.collider.GetComponentInParent<AI>().PlayEffect(Hit);
            }
            else
            {
                GameObject.Instantiate<GameObject>(m_AssaultRifleView.Bullet, Hit.point,Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("无子弹生成");
        }
        Durable--;
    }



    protected override void LoadAudioAssets()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/AssaultRifle_Fire");
    }

    protected override void LoadEffectAssets()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/AssaultRifle_GunPoint_Effect");
    }
}
