using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class InputManager : MonoBehaviour {
    public static InputManager Instance;

    public bool InventroyState = false;

    //private GunControllerBase m_GunControllerBase;
    private FirstPersonController m_FirstPersonController;
    private GameObject m_BuildPanel;

    private bool isBuild;

    public bool IsBuild
    {
        get
        {
            return isBuild;
        }

        set
        {
            if (m_BuildPanel != null)
            {
                m_BuildPanel.SetActive(value);
            }
            isBuild = value;
            if (isBuild == true)
            {
                BuildPanelController.Instance.ResetUI();
            }
            else
            {
                BuildPanelController.Instance.DestroyBuildModel();
            }
        }
    }

    //private GameObject m_Gunstar;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        m_BuildPanel = GameObject.Find("Canvas/BuildPanel");
        m_BuildPanel.SetActive(false);
        InventroyPanelController.Instance.UIPanelHide();
        FindInit();
    }
	
	
	void Update ()
    {
        InventoryPanelKey();
        ToolBarPanelKey();

        
    }

    private void FindInit()
    {
        //m_GunControllerBase = GameObject.Find("FPSController/PersonCamera/Wooden Bow").GetComponent<GunControllerBase>();
        m_FirstPersonController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        //m_Gunstar = GameObject.Find("Canvas/MainPanel/GunStar");
    }





      

    /// <summary>
    /// 背包面板按键检测
    /// </summary>
    private void InventoryPanelKey()
    {
        if (Input.GetKeyDown(GameConst.InventroyPanelKey) && IsBuild == false)
        {
            if (InventroyState)     //关闭背包
            {
                InventroyState = false;
                InventroyPanelController.Instance.UIPanelHide();
                m_FirstPersonController.enabled = true;
                if (ToolBarPanelController.Instance.CurrentActiveModel != null)
                {
                    ToolBarPanelController.Instance.CurrentActiveModel.SetActive(true);
                }
                
            }
            else                    //打开背包
            {
                InventroyState = true;


                InventroyPanelController.Instance.UIPanelShow();
                if (ToolBarPanelController.Instance.CurrentActiveModel != null)
                {
                    ToolBarPanelController.Instance.CurrentActiveModel.SetActive(false);
                }
                //打开背包,主场景内的任何操作都将被禁用
                m_FirstPersonController.enabled = false;
                //锁定状态改为None
                Cursor.lockState = CursorLockMode.None;
                //设置可见
                Cursor.visible = true;
            }
        }
    }

    /// <summary>
    /// 工具栏按键检测
    /// </summary>
    private void ToolBarPanelKey()
    {
        if (InventroyState)
        {
            return;
        }

        ToolBarKey(GameConst.ToolBarPanelKey_1,0);
        ToolBarKey(GameConst.ToolBarPanelKey_2,1);
        ToolBarKey(GameConst.ToolBarPanelKey_3,2);
        ToolBarKey(GameConst.ToolBarPanelKey_4,3);
        ToolBarKey(GameConst.ToolBarPanelKey_5,4);
        ToolBarKey(GameConst.ToolBarPanelKey_6,5);
        ToolBarKey(GameConst.ToolBarPanelKey_7,6);
        ToolBarKey(GameConst.ToolBarPanelKey_8,7);
    }

    /// <summary>
    /// 单个按键检测
    /// </summary>
    private void ToolBarKey(KeyCode keyCode,int ketNum)
    {
        if (Input.GetKeyDown(keyCode))
        {
            ToolBarPanelController.Instance.SaveActiveSlotByKey(ketNum);
        }
    }
}
