using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBow : ThroWeaponBase
{
    private WoodenBowView m_WoodenBowView;

    protected override void Init()
    {
        m_WoodenBowView = (WoodenBowView)M_GunViewBase;
    }

    protected override void LoadAudioAssets()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }



    protected override void Shoot()
    {
        CanShoot(0);
        GameObject tempArrow = GameObject.Instantiate<GameObject>(m_WoodenBowView.Arrow, m_WoodenBowView.GunPoint.position, m_WoodenBowView.GunPoint.rotation);
        tempArrow.GetComponent<Arrow>().Shoot(m_WoodenBowView.GunPoint.forward,1000,Damage , Hit);
        Durable--;
    }
}
