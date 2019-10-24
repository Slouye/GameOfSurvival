using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerTrigger : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            gameObject.transform.parent.eulerAngles += new Vector3(0, -90, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            gameObject.transform.parent.eulerAngles += new Vector3(0, 90, 0);
        }
    }
   
    
}
