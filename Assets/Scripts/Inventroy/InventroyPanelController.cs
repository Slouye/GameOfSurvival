using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包控制器
/// </summary>
public class InventroyPanelController : MonoBehaviour,IUIPanelShowHide {
    public static InventroyPanelController Instance;


    //获取视图，数据对象
    InventroyPanelView m_InventroyPanelView;
    InventroyPanelModel m_InventroyPanelModel;
    int slotNum = 27;
    List<GameObject> slotLis = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        m_InventroyPanelView = gameObject.GetComponent<InventroyPanelView>();

        m_InventroyPanelModel = gameObject.GetComponent<InventroyPanelModel>();

        CreateAllSlot();
        CreateAllItem();
    }
	
    /// <summary>
    /// 创建所有背包槽
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < slotNum; i++)
        {
            GameObject tempSlot = GameObject.Instantiate<GameObject>(m_InventroyPanelView.Prefab_Slot, m_InventroyPanelView.GridTransform);
            tempSlot.name = "InventroySlot" + i;
            slotLis.Add(tempSlot);
        }
    }

    /// <summary>
    /// 生成全部物品
    /// </summary>
    private void CreateAllItem()
    {
        List<InventroyItem> tempList = m_InventroyPanelModel.GetJsonList("InventoryJsonData");
        
        for (int i = 0; i < tempList.Count; i++)
        {
            GameObject temp = GameObject.Instantiate<GameObject>(m_InventroyPanelView.Prefab_Item, slotLis[i].GetComponent<Transform>());
            temp.GetComponent<InventroyItemController>().InitItem(tempList[i].ItemName,tempList[i].ItemNum, tempList[i].ItemId, tempList[i].ItemBar);
        }
    }

    /// <summary>
    /// 往背包放入材料
    /// </summary>
    public void AddItems(List<GameObject> itemList)
    {
        int tempIndex = 0;
        for (int i = 0; i < slotLis.Count; i++)
        {
            if (slotLis[i].transform.Find("InventroyItem") == null && tempIndex < itemList.Count)
            {
                itemList[tempIndex].transform.SetParent(slotLis[i].transform);
                itemList[tempIndex].GetComponent<InventroyItemController>().InInentroy = true;
                tempIndex++;
            }
        }
    }

    public void SendDargMaterialsItem(GameObject item)
    {
        CraftingPanelController.Instance.DargMaterialsItem(item);
    }

    public void UIPanelShow()
    {
        gameObject.SetActive(true);
    }

    public void UIPanelHide()
    {
        gameObject.SetActive(false);
    }
}
