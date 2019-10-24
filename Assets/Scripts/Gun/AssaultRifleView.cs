using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssaultRifleView : GunViewBase {
    private Transform effectPos;        //弹壳位置

    private GameObject bullet;          //子弹
    private GameObject shell;           //弹壳

    private Transform effectParent;     //枪火特效父物体
    private Transform shellParent;      //弹壳特效父物体
    
    public Transform EffectPos { get { return effectPos; } }
    public Transform M_effectParent { get { return effectParent; } }
    public Transform M_shellParent { get { return shellParent; } }
    
    public GameObject Bullet { get { return bullet; } }
    public GameObject Shell { get { return shell; } }



    protected override void InitHoldPoseValue()
    {
        StartPos = M_Transform.localPosition;
        StartRot = M_Transform.localRotation.eulerAngles;
        EndPos = new Vector3(-0.065f, -1.85f, 0.25f);
        EndRot = new Vector3(2.8f, 1.3f, 0.08f);
    }

    protected override void FindGunPoint()
    {
        GunPoint = M_Transform.Find("Assault_Rifle/EffectPos_A");
    }

    protected override void Init()
    {
        effectPos = M_Transform.Find("Assault_Rifle/EffectPos_B");
        effectParent = GameObject.Find("TempObject/AssaultRifle_Effect_Parent").GetComponent<Transform>();
        shellParent = GameObject.Find("TempObject/AssaultRifle_Shell_Parent").GetComponent<Transform>();

        bullet = Resources.Load<GameObject>("Gun/Bullet");
        shell = Resources.Load<GameObject>("Gun/Shell");
    }
}
