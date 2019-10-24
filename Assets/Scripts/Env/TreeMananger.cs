using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMananger : MonoBehaviour {
    private Transform m_Transform;
    private Transform[] Points;
    private Transform Tree_Transform;

    private GameObject Prefab_Tree;
    private GameObject Prefab_Tree_1;
    private GameObject Prefab_Tree_2;
    // Use this for initialization
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        Tree_Transform = m_Transform.Find("Trees");
        Prefab_Tree = Resources.Load<GameObject>("Env/Broadleaf");
        Prefab_Tree_1 = Resources.Load<GameObject>("Env/Conifer");
        Prefab_Tree_2= Resources.Load<GameObject>("Env/Palm");
        Points = m_Transform.Find("TreePoints").GetComponentsInChildren<Transform>();


        for (int i = 1; i < Points.Length; i++)
        {
            int index = Random.Range(0, 3);
            GameObject prefab = null;
            if (index == 0)
            {
                prefab = Prefab_Tree;
            }
            else if(index == 1)
            {
                prefab = Prefab_Tree_1;
            }
            else if (index == 2)
            {
                prefab = Prefab_Tree_2;
            }
            Points[i].GetComponent<MeshRenderer>().enabled = false;
            Transform tree = GameObject.Instantiate<GameObject>(prefab, Points[i].localPosition, Quaternion.identity, Tree_Transform).transform;
            tree.localScale = tree.localScale * Random.Range(0.2f, 1f);
            tree.localRotation = tree.localRotation * Quaternion.Euler(0, Random.Range(0, 360), 0);
            tree.gameObject.name = "Tree";
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
