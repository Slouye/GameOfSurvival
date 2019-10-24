using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanelController : MonoBehaviour {
    public static CraftingPanelController Instance;

    private Transform m_Transform;

    private CraftingPanelView m_CraftingPanelView;
    private CraftingPanelModel m_CraftingPanelModel;
    private CraftingController m_CraftingController;

    private int tabNum = 2;
    private int slotNum = 25;
    private int current = -1;
    private int materialsCount = 0;         //合成物品所需材料数，JSON
    private int dargMaterialsCount = 0;     //合成图谱所放入的材料数

    private List<GameObject> tabsList;
    private List<GameObject> contentsList;
    private List<GameObject> slotsList;
    private List<GameObject> materialsList; //管理拖拽放入的材料

    

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        Init();
        CreateAllTabs();
        CreateAllContents();
        ResetTabsAndContents(0);
        CreateAllSlot();
        //CreateSlotContents();

       
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        m_CraftingPanelView = gameObject.GetComponent<CraftingPanelView>();
        m_CraftingPanelModel = gameObject.GetComponent<CraftingPanelModel>();
        m_CraftingController = m_Transform.Find("Right").GetComponent<CraftingController>();


        tabsList = new List<GameObject>();
        contentsList = new List<GameObject>();
        slotsList = new List<GameObject>();
        materialsList = new List<GameObject>();

        m_CraftingController.Prefab_InventroyItem = m_CraftingPanelView.Prefab_InventroyItem;
    }

    /// <summary>
    /// 创建所有选项卡
    /// </summary>
    private void CreateAllTabs()
    {
        for (int i = 0; i < tabNum; i++)
        {
           
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Tabs_Item, m_CraftingPanelView.Tabs_Transform);
            Sprite temp = m_CraftingPanelView.ByNameGetSprites(m_CraftingPanelModel.GetTabsIconName()[i]);
            go.GetComponent<CraftingPanelTabItemController>().InitItem(i, temp);
            tabsList.Add(go);
        }
    }
    
    //创建所有正文
    private void CreateAllContents()
    {
        List<List<CraftingContentItem>> tempList = m_CraftingPanelModel.ByNameGetJsonData("CraftingContentsJsonData");
        for (int i = 0; i < tabNum; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Crafting_Content, m_CraftingPanelView.Contents_Transform);
            go.GetComponent<CraftingContent>().InitContent(i, m_CraftingPanelView.Crafting_ContentItem,tempList[i]);
            contentsList.Add(go);

        }
    }

    /// <summary>
    /// 重置选项卡和正文区域
    /// </summary>
    /// <param name="index"></param>
    private void ResetTabsAndContents(int index)
    {
        if (index == current)
        {
            return;
        }
        for (int i = 0; i < tabsList.Count; i++)
        {
            tabsList[i].GetComponent<CraftingPanelTabItemController>().NoramlTab();
            contentsList[i].SetActive(false);
        }
        tabsList[index].GetComponent<CraftingPanelTabItemController>().ActiveTab();
        contentsList[index].SetActive(true);
        current = index;
    }

    /// <summary>
    /// 创建所有材料槽
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < slotNum; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Crafting_Slot,m_CraftingPanelView.Center_Transform);
            go.name = "Slot" + i;
            slotsList.Add(go);
        }
    }

    /// <summary>
    /// 填充材料槽数据
    /// </summary>
    private void CreateSlotContents(int id)
    {
        CraftingMapItem temp = m_CraftingPanelModel.ByIdGetMapItem(id);
        if (temp != null)
        {
            //清空上一次图谱
            ResetSlotContents();

            //把图谱材料重新放回背包
            ResetMaterials();

            for (int j = 0; j < temp.MapContents.Length; j++)
            {
                if (temp.MapContents[j] != "0")
                {
                    Sprite sp = m_CraftingPanelView.ByNameGetMaterialIconSprites(temp.MapContents[j]);
                    slotsList[j].GetComponent<CraftingSlotController>().Init(sp, temp.MapContents[j]);
                }
            }
            //最终合成物品图标显示
            m_CraftingController.Init(temp.MapId,temp.MapName);
            //保存需要合成物品的材料数
            materialsCount = temp.MaterialsCount;
        }
        
    }

    /// <summary>
    /// 重置图谱内容
    /// </summary>
    private void ResetSlotContents()
    {
        for (int i = 0; i < slotsList.Count; i++)
        {
            slotsList[i].GetComponent<CraftingSlotController>().Reset();
            slotsList[i].GetComponent<CraftingSlotController>().Id = 0;
        }
    }

    /// <summary>
    /// 重置合成图谱内的材料,把材料放回到背包中
    /// </summary>
    private void ResetMaterials()
    {
        List<GameObject> materialsList = new List<GameObject>();
        for (int i = 0; i < slotsList.Count; i++)
        {
            if (slotsList[i].transform.Find("InventroyItem") != null)
            {
                materialsList.Add(slotsList[i].transform.Find("InventroyItem").gameObject);
            }
        }
        InventroyPanelController.Instance.AddItems(materialsList);
    }

    public void DargMaterialsItem(GameObject item)
    {
        materialsList.Add(item);
        dargMaterialsCount++;
        Debug.Log(materialsCount + "---" + dargMaterialsCount);

        //激活合成按钮
        if (materialsCount == dargMaterialsCount)
        {
            m_CraftingController.ActiveButton();
        }
    }

    //合成物品成功==>剩余材料返回背包栏
    private void CraftingOK()
    {
        for (int i = 0; i < materialsList.Count; i++)
        {
            InventroyItemController iic = materialsList[i].GetComponent<InventroyItemController>();
            iic.target = null;
            if (iic.Num == 1)
            {
                GameObject.Destroy(iic.gameObject);
            }
            else
            {
                iic.Num = iic.Num - 1;
            }
        }
        StartCoroutine(ResetMap());
    }

    private IEnumerator ResetMap()
    {
        yield return new WaitForSeconds(0);
        ResetMaterials();
        dargMaterialsCount = 0;
        materialsList.Clear();
    }
}
