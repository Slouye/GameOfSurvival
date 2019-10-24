using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof : BuildModelBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WallToRoof")
        {
            IsCunPut = true;
            IsAttach = true;
            gameObject.transform.position = other.transform.position;
        }

        if (other.gameObject.tag == "Roof")
        {
            IsCunPut = true;
            IsAttach = true;
            Vector3 targetPos = other.gameObject.transform.parent.position;
            Vector3 modlePos = Vector3.zero;
            Debug.Log(other.gameObject.name);
            switch (other.gameObject.name)
            {
                case "A":
                    modlePos = new Vector3(-3.3f, 0, 0);
                    break;
                case "B":
                    modlePos = new Vector3(0, 0, 3.3f);
                    break;
                case "C":
                    modlePos = new Vector3(3.3f, 0, 0);
                    break;
                case "D":
                    modlePos = new Vector3(0, 0, -3.3f);
                    break;
            }
            gameObject.transform.position = targetPos + modlePos;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WallToRoof")
        {
            IsCunPut = false;
            IsAttach = false;
        }
        if (other.gameObject.tag == "Roof")
        {
            IsCunPut = false;
            IsCunPut = false;
        }

    }
}
