using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling_Light : BuildModelBase
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
        if (other.gameObject.tag == "LightTrigger")
        {
            IsCunPut = true;
            IsAttach = true;
            gameObject.transform.position = other.transform.position;
            Trigger = other.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LightTrigger")
        {
            IsCunPut = false;
            IsAttach = false;
        }
    }
}
