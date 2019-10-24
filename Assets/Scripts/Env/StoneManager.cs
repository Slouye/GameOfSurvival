using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour {
    private Transform m_Transform;
    private Transform[] Points;
    private Transform Stone_Transform;

    private GameObject Prefab_Stone;
    private GameObject Prefab_Stone_1;
    // Use this for initialization
    void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        Stone_Transform = m_Transform.Find("Stones");
        Prefab_Stone = Resources.Load<GameObject>("Env/Rock_Normal");
        Prefab_Stone_1 = Resources.Load<GameObject>("Env/Rock_Metal");
        Points = m_Transform.Find("StonePoints").GetComponentsInChildren<Transform>();
      

        for (int i = 1; i < Points.Length; i++)
        {
            int index = Random.Range(0, 2);
            GameObject prefab;
            if (index == 0)
            {
                prefab = Prefab_Stone;
            }
            else
            {
                prefab = Prefab_Stone_1;
            }
            Points[i].GetComponent<MeshRenderer>().enabled = false;
            Transform stone = GameObject.Instantiate<GameObject>(prefab, Points[i].localPosition ,Quaternion.identity, Stone_Transform).transform;
            stone.localScale = stone.localScale * Random.Range(0.5f, 2.5f);
            stone.localRotation = stone.localRotation * Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
