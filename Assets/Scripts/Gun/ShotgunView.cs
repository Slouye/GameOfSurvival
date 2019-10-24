using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunView : GunViewBase
{
    private Transform effectPos;        //弹壳位置
    private AudioClip audioEffect;      //特效声音

    private GameObject shell;           //弹壳
    private GameObject bullet;           //子弹


    public AudioClip AudioEffect { get { return audioEffect; } }

    public Transform EffectPos { get { return effectPos; } }



    public GameObject Shell { get { return shell; } }
    public GameObject Bullet { get { return bullet; } }

    protected override void FindGunPoint()
    {
        GunPoint = M_Transform.Find("Armature/Weapon/EffectPos_A");
    }

    protected override void Init()
    {
        effectPos = M_Transform.Find("Armature/Weapon/EffectPos_B");
        audioEffect = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Pump");
        shell = Resources.Load<GameObject>("Gun/Shotgun_Shell");
        bullet = Resources.Load<GameObject>("Gun/Shotgun_Bullet");
    }

    protected override void InitHoldPoseValue()
    {
        StartPos = M_Transform.localPosition;
        StartRot = M_Transform.localRotation.eulerAngles;
        EndPos = new Vector3(-0.14f, -1.78f, -0.03f);
        EndRot = new Vector3(0, 10f, -0.25f);
    }
}
