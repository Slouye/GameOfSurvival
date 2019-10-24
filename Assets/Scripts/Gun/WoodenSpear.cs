using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpear : ThroWeaponBase
{
    private WoodenSpearView m_WoodenSpearView;

    protected override void Init()
    {
        m_WoodenSpearView = (WoodenSpearView)M_GunViewBase;
    }

    protected override void LoadAudioAssets()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }



    protected override void Shoot()
    {
        CanShoot(0);
        GameObject tempSpear = GameObject.Instantiate<GameObject>(m_WoodenSpearView.Spear, m_WoodenSpearView.GunPoint.position, m_WoodenSpearView.GunPoint.rotation);
        tempSpear.GetComponent<Arrow>().Shoot(m_WoodenSpearView.GunPoint.forward, 1000, Damage, Hit);
        Durable--;
    }
}
