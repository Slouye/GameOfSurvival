

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolBarPanelController : MonoBehaviour {
    public static ToolBarPanelController Instance;

    private ToolBarPanelView m_ToolBarPanelView;
    private ToolBarPanelModel m_ToolBarPanelModel;

    private GameObject currentActive = null;        //当前槽的物品
    private List<GameObject> slotList = null;

    private GameObject currentActiveModel;          //点前使用的角色
    private Dictionary<GameObject, GameObject> toolBarDic;

    private int currentKeyCode = -1;

    public GameObject CurrentActiveModel { get { return currentActiveModel; } }

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        Init();
        CreateAllSlot();
    }
	
	
	void Update () {
		
	}

    private void Init()
    {
        m_ToolBarPanelView = gameObject.GetComponent<ToolBarPanelView>();
        m_ToolBarPanelModel = gameObject.GetComponent<ToolBarPanelModel>();
        slotList = new List<GameObject>();
        toolBarDic = new Dictionary<GameObject, GameObject>();
    }

    /// <summary>
    /// 创建所有物品槽
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject slot = GameObject.Instantiate<GameObject>(m_ToolBarPanelView.Prefab_ToolBarSlot, m_ToolBarPanelView.Grid_Transform);
            slot.GetComponent<ToolBarSlotController>().Init(m_ToolBarPanelView.Prefab_ToolBarSlot.name + i, i + 1);
            slotList.Add(slot);
        }
    }

    /// <summary>
    /// 保存当前选中的物品槽及物品，选中状态切换
    /// </summary>
    /// <param name="activeSlot"></param>
    private void SaveActiveSlot(GameObject activeSlot)
    {
        if (currentActive != null && currentActive != activeSlot)
        {
            currentActive.GetComponent<ToolBarSlotController>().Normal();
            currentActive = null;
        }
        currentActive = activeSlot;
    }

    public void SaveActiveSlotByKey(int keyNum)
    {
        //if (currentActive != null && currentActive != slotList[keyNum])
        //{
        //    currentActive.GetComponent<ToolBarSlotController>().Normal();
        //    currentActive = null;
        //}
        //currentActive = slotList[keyNum];
        //currentActive.GetComponent<ToolBarSlotController>().SlotClick();

        //检测工具槽内是否有物品
        if (slotList[keyNum].transform.Find("InventroyItem") == null)
        {
            return;
        }

        GameObject temp = slotList[keyNum];
        temp.GetComponent<ToolBarSlotController>().SlotClick();

        if (currentKeyCode == keyNum && currentActiveModel != null)
        {
            //卸下武器
            CurrentActiveModel.SetActive(false);
            currentActiveModel = null;
        }
        else
        {
            //切换武器
            Transform tempTransform = currentActive.transform.Find("InventroyItem");
            StartCoroutine(CallGunFactory(tempTransform));
        }
        currentKeyCode = keyNum;
    }

    //工厂模式创建武器角色
    IEnumerator CallGunFactory(Transform tempTransform)
    {
        if (tempTransform != null)
        {
            if (CurrentActiveModel != null)
            {
                if (CurrentActiveModel.tag != "Building")
                {
                    CurrentActiveModel.GetComponent<GunControllerBase>().Holster();
                   
                }
                yield return new WaitForSeconds(0.7f);
                CurrentActiveModel.SetActive(false);
            }

            //检查物品重复创建
            GameObject temp = null;
            toolBarDic.TryGetValue(tempTransform.gameObject,out temp);

            if (temp == null)
            {
                temp = GunFactory.Instance.CreateGun(tempTransform.GetComponent<Image>().sprite.name, tempTransform.gameObject);
                toolBarDic.Add(tempTransform.gameObject, temp);
            }
            else
            {
                //卸载枪械
                if (currentActive.GetComponent<ToolBarSlotController>().SelfState)
                {
                    temp.SetActive(true);
                }
            }
            currentActiveModel = temp;
        }
        
    }
}
