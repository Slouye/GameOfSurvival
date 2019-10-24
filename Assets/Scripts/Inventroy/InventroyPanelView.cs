using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包视图控制
/// </summary>
public class InventroyPanelView : MonoBehaviour {
    Transform m_Transform;
    Transform grid_Transform;
    GameObject prefab_Slot;
    GameObject prefab_Item;

    public Transform Transform { get { return m_Transform; }}

    public Transform GridTransform { get { return grid_Transform; }}

    public GameObject Prefab_Slot { get { return prefab_Slot; }}

    public GameObject Prefab_Item { get { return prefab_Item; }}


 

 


    void Awake() {
        m_Transform = gameObject.GetComponent<Transform>();
        grid_Transform = m_Transform.Find("Background/Grid");
        prefab_Slot = Resources.Load<GameObject>("InventroySlot");
        prefab_Item = Resources.Load<GameObject>("InventroyItem");

    }
	
}
