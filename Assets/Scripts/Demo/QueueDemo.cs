using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueDemo : MonoBehaviour {
    private GameObject prefab_Cube;
    private float index;
    private Queue<GameObject> CubeQueue;
    void Start () {
        prefab_Cube = Resources.Load<GameObject>("Cube");
        CubeQueue = new Queue<GameObject>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            index++;
            GameObject go = GameObject.Instantiate<GameObject>(prefab_Cube, new Vector3(index*1.2f, 0,0), Quaternion.identity);
            go.name = "Cube" + index;
            CubeQueue.Enqueue(go);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (CubeQueue.Count > 0)
            {
                GameObject.Destroy(CubeQueue.Dequeue());
            }
            
        }
    }
}
