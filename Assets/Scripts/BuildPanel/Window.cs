using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : BuildModelBase
{

    GameObject Trigger;
    public override void Normal()
    {
        base.Normal();
        if (Trigger != null)
        {
            GameObject.Destroy(Trigger);
        }

    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WallToWindow")
        {
            IsCunPut = true;
            IsAttach = true;
            gameObject.transform.position = other.transform.position;
            gameObject.transform.rotation = other.transform.parent.rotation;
            Trigger = other.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WallToWindow")
        {
            IsCunPut = false;
            IsAttach = false;
        }
    }
}
