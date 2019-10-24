

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : BuildModelBase
{
    GameObject Trigger = null;
    public override void Normal()
    {
        base.Normal();
        if (Trigger != null)
        {
            Trigger.name = "E";
        }

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlatformWall")
        {
            IsCunPut = true;
            IsAttach = true;

            Vector3 targetPos = other.gameObject.transform.parent.position;
            Vector3 modlePos = Vector3.zero;
            Vector3 modleRot = Vector3.zero;
            Trigger = other.gameObject;

            switch (other.gameObject.name[0].ToString())
            {
                case "A":
                    //前
                    modlePos = new Vector3(0, 0, 2.5f);
                    modleRot = new Vector3(0, 90, 0);
                    break;
                case "B":
                    //左
                    modlePos = new Vector3(-2.5f, 0, 0);
                    modleRot = new Vector3(0, 0, 0);
                    break;
                case "C":
                    //后
                    modlePos = new Vector3(0, 0, -2.5f);
                    modleRot = new Vector3(0, -90, 0);
                    break;
                case "D":
                    //右
                    modlePos = new Vector3(2.5f, 0, 0);
                    modleRot = new Vector3(0, 180, 0);
                    break;
            }
            gameObject.transform.position = targetPos + modlePos;
            gameObject.transform.rotation = Quaternion.Euler(modleRot);
        }

    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlatformWall")
        {
            IsCunPut = false;
            IsAttach = false;
        }
    }
}
