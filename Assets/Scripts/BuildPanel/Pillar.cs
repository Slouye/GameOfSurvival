using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : BuildModelBase
{
    

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlatformToPillar")
        {
            IsCunPut = true;
            IsAttach = true;
            gameObject.transform.position = other.transform.position;
           
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlatformToPillar")
        {
            IsCunPut = false;
            IsAttach = false;
        }
    }
}
