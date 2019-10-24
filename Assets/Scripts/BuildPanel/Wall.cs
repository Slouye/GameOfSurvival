using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BuildModelBase
{

    GameObject Trigger = null;
    public override void Normal()
    {
        base.Normal();
        if (Trigger != null)
        {
            Trigger.name = Trigger.name + "_Over";
        }

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlatformWall" && other.gameObject.name.Length ==1)
        {
            IsCunPut = true;
            IsAttach = true;
            gameObject.transform.position = other.transform.position;
            gameObject.transform.rotation = other.transform.rotation;
            Trigger = other.gameObject;
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
