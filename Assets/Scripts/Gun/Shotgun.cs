using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunWeaponBase
{
    private ShotgunView m_ShotgunView;

    protected override void Init()
    {
        m_ShotgunView = (ShotgunView)M_GunViewBase;
    }

    protected override void LoadAudioAssets()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Fire");
    }

    protected override void LoadEffectAssets()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/Shotgun_GunPoint_Effect");
    }

    protected override void PlayEffect()
    {
        //枪口特效
        GameObject tempEffect = GameObject.Instantiate<GameObject>(Effect, M_GunViewBase.GunPoint.position, Quaternion.identity);
        tempEffect.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DelayDestroy(tempEffect,2));

        //弹壳弹出特效
        GameObject tempShell = GameObject.Instantiate<GameObject>(m_ShotgunView.Shell, m_ShotgunView.EffectPos.position, Quaternion.identity);
        tempShell.GetComponent<Rigidbody>().AddForce(m_ShotgunView.EffectPos.up * 70);
        StartCoroutine(DelayDestroy(tempShell, 5));
    }

    IEnumerator DelayDestroy(GameObject go,float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(go);
    }

    protected override void Shoot()
    {
        StartCoroutine(CreateBullet());
        Durable--;
    }

    IEnumerator CreateBullet()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 offest = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0);
            GameObject tempBullet = GameObject.Instantiate<GameObject>(m_ShotgunView.Bullet, m_ShotgunView.GunPoint.position, Quaternion.identity);
            tempBullet.GetComponent<ShotgunBullet>().Shoot(m_ShotgunView.GunPoint.forward + offest, 3000,Damage/5, Hit);
            //tempBullet.GetComponent<ShotgunBullet>().Shoot(m_ShotgunView.GunPoint.forward, 3000);
            yield return new WaitForSeconds(0.03f);
        }
    }

    /// <summary>
    /// 播放散弹卡壳音效
    /// </summary>
    private void PlayEffectAudio()
    {
        AudioSource.PlayClipAtPoint(m_ShotgunView.AudioEffect, m_ShotgunView.EffectPos.position);
    }
}
