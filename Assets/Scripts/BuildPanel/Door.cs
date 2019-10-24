using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BuildModelBase
{
    GameObject Trigger = null;
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
        if (other.gameObject.name == "DoorTrigger")
        {
            IsCunPut = true;
            IsAttach = true;

            gameObject.transform.position = other.transform.parent.Find("DoorPos").position;
            gameObject.transform.rotation = other.transform.rotation;
            Trigger = other.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "DoorTrigger")
        {
            IsCunPut = false;
            IsAttach = false;
            Trigger = null;
        }
    }
}
