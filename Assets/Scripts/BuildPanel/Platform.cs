

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : BuildModelBase {
    string indexName;
    Transform targetTransform;

    public override void Normal()
    {
        base.Normal();
        //if (indexName != null)
        //{
        //    GameObject.Destroy(targetTransform.Find(indexName).gameObject);
        //    switch (indexName)
        //    {
        //        case "A":
        //            GameObject.Destroy(gameObject.transform.Find("C").gameObject);
        //            break;
        //        case "B":
        //            GameObject.Destroy(gameObject.transform.Find("D").gameObject);
        //            break;
        //        case "C":
        //            GameObject.Destroy(gameObject.transform.Find("A").gameObject);
        //            break;
        //        case "D":
        //            GameObject.Destroy(gameObject.transform.Find("B").gameObject);
        //            break;
        //    }
        //}
        

    }


    private void Start()
    {
        IsCunPut = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Terrain")
        {
            IsCunPut = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "Terrain")
        {
            IsCunPut = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag != "Terrain")
        {
            IsCunPut = true;
        }
    }




    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlatformWall")
        {
            IsAttach = true;
            Vector3 modlePos = Vector3.zero;
            Vector3 targetPos = other.gameObject.transform.parent.position;
            targetTransform = other.gameObject.transform.parent;


            switch (other.gameObject.name)
            {
                case "A":
                    //前
                    modlePos = new Vector3(0, 0, 3.3f);
                    indexName = "A";
                    break;
                case "B":
                    //左
                    modlePos = new Vector3(-3.3f, 0, 0);
                    indexName = "B";
                    break;
                case "C":
                    //后
                    modlePos = new Vector3(0, 0, -3.3f);
                    indexName = "C";
                    break;
                case "D":
                    //右
                    modlePos = new Vector3(3.3f, 0, 0);
                    indexName = "D";
                    break;
            }


            gameObject.transform.position = modlePos + targetPos;
            

        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            IsAttach = false;
        }
    }
}
