

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelController : MonoBehaviour {
    public static BuildPanelController Instance;


    private BuildPanelView m_BuildPanelView;
    
    private bool isShow = true;         //环形ui显示隐藏标志位

    //滚轮属性————Item
    private float scrollNum = -90000.0f;     //用于累加记录滚轮的滚动值
    private int index = 0;              //索引
    private Item currentItem = null;    //当前选中
    private Item targetItem = null;     //下一个选中

    //滚轮属性————Material
    private float scrollNum_Material = 90000.0f;     //用于累加记录滚轮的滚动值
    private int index_Material = 0;              //索引
    private MaterialItem currentItem_Material = null;    //当前选中
    private MaterialItem targetItem_Material = null;     //下一个选中
    
    private int zIndex = 20;            //材料初始旋转
    private bool isItemCtr = true;      //识别当前操作的是Item还是Material
    private GameObject tempBuildModel = null;       //临时当前建造的模型
    public GameObject BuildModel = null;           //当前建造的模型
    private Ray ray;
    private RaycastHit hit;
    private int indexLayer = 0;          //射线检测层


    private List<Item> itemList = new List<Item>();
    private string[] itemNames = { "", "[杂项]", "[屋顶]", "[楼梯]", "[窗户]", "[门]", "[墙壁]", "[地板]", "[地基]" };


    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        m_BuildPanelView = gameObject.GetComponent<BuildPanelView>();
        CreateItems();
    }
	
	void Update () {
        
            MouseLeft();
            MouseRight();
            MouseScroll();
            if (BuildModel != null) SetModelPosition();
        
    }

    /// <summary>
    /// 鼠标左键
    /// </summary>
    private void MouseLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tempBuildModel != null)
            {
                if (targetItem.materialList.Count == 0)
                {
                    SetLeftKeyNull();
                    m_BuildPanelView.M_bg_Transform.gameObject.SetActive(false);
                    isShow = false;
                    return;
                }
            }

            if (tempBuildModel == null) isItemCtr = false;

            if (tempBuildModel != null && isShow)
            {
                m_BuildPanelView.M_bg_Transform.gameObject.SetActive(false);
                isShow = false;
            }
            

            if (tempBuildModel != null)
            {
                if (tempBuildModel.GetComponent<BuildModelBase>() != null)
                {

                    //不可放置
                    if (BuildModel != null && BuildModel.GetComponent<BuildModelBase>().IsCunPut == false)
                    {
                        return;
                    }

                    //可放置
                    if (BuildModel != null && BuildModel.GetComponent<BuildModelBase>().IsCunPut)
                    {
                        BuildModel.name = tempBuildModel.name;
                        BuildModel.layer = 14;
                        BuildModel.GetComponent<BuildModelBase>().Normal();
                        GameObject.Destroy(BuildModel.GetComponent<BuildModelBase>());
                    }
                }
            }

            if (tempBuildModel != null)
            {
                BuildModel = GameObject.Instantiate<GameObject>(tempBuildModel, m_BuildPanelView.M_player_Transform.position + new Vector3(0, 0, 10), Quaternion.identity, m_BuildPanelView.M_models_Parent);
                isItemCtr = true;
            }

        }
    }

    /// <summary>
    /// 鼠标右键
    /// </summary>
    private void MouseRight()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isItemCtr == false)
            {
                //二级工具
                if (currentItem_Material != null)
                {
                    currentItem_Material.Normal();
                    isItemCtr = true;
                }
                else
                {
                    ShowOrHide();
                }

            }
            else
            {
                //一级工具
                isItemCtr = false;
                SetLeftKeyNull();
                ShowOrHide();
            }

        }
    
        
        
    }


    /// <summary>
    /// 鼠标滚轮
    /// </summary>
    private void MouseScroll()
    {
        //鼠标滚轮---Item
        if (isShow && isItemCtr)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                MouseScrollWheel();
            }
        }

        //鼠标滚轮---Material
        if (isShow && isItemCtr == false)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                MouseScrollWheelMaterial();
            }
        }
    }



    /// <summary>
    /// 创建所有Item
    /// </summary>
    private void CreateItems()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject item = GameObject.Instantiate<GameObject>(m_BuildPanelView.M_prefab_Item, m_BuildPanelView.M_bg_Transform);

            if (m_BuildPanelView.Icons[i] == null)
            {
                item.GetComponent<Item>().Init("item", Quaternion.Euler(new Vector3(0, 0, i * 40)), false, null, true);
            }
            else
            {
                item.GetComponent<Item>().Init("item", Quaternion.Euler(new Vector3(0, 0, i * 40)), true, m_BuildPanelView.Icons[i], false);
                
                //生成分类具体建筑
                for (int j = 0; j < m_BuildPanelView.MaterialIcons[i].Length; j++)
                {
                    GameObject material = GameObject.Instantiate<GameObject>(m_BuildPanelView.M_prefab_MaterialsBG, m_BuildPanelView.M_bg_Transform);
                    zIndex += 13;
                    material.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, zIndex));
                    if (m_BuildPanelView.MaterialIcons[i][j] == null)
                    {
                        material.transform.Find("Icon").GetComponent<Image>().enabled = false;
                    }
                    else
                    {
                        material.transform.Find("Icon").GetComponent<Image>().sprite = m_BuildPanelView.MaterialIcons[i][j];
                    }
                    material.name = "MaterialsBG";
                    material.transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);
                    item.GetComponent<Item>().MaterialListAdd(material);
                    item.GetComponent<Item>().Hide();
                }
            }
            itemList.Add(item.GetComponent<Item>());
           
        }
        currentItem = itemList[0];
        SetTextValue();
    }

    /// <summary>
    /// 环形UI隐藏于显示
    /// </summary>
    private void ShowOrHide()
    {
        if (isShow)
        {
            m_BuildPanelView.M_bg_Transform.gameObject.SetActive(false);
            isShow = false;
        }
        else
        {
            m_BuildPanelView.M_bg_Transform.gameObject.SetActive(true);
            isShow = true;
            if (tempBuildModel != null) tempBuildModel = null;
            if (targetItem_Material != null)
            {
                targetItem_Material.Normal();
            }


            ResetUI();
        }
    }

    //重置ui
    public void ResetUI()
    {
        DestroyBuildModel();
        if (tempBuildModel != null) tempBuildModel = null;
        if (currentItem != null)
        {
            currentItem.Hide();
            currentItem = itemList[0];
            currentItem.Show();
        }
        index = 0;
        scrollNum = -90000.0f;
        
    }

    public void DestroyBuildModel()
    {
        GameObject.Destroy(BuildModel);
    }

    public void DestroyTempBuildModel()
    {
        if (tempBuildModel != null)
        {
            GameObject.Destroy(tempBuildModel);
        }
    }

    /// <summary>
    /// 鼠标滚轮操作---Item
    /// </summary>
    private void MouseScrollWheel()
    {
        scrollNum += Input.GetAxis("Mouse ScrollWheel") * 5;
        index = Mathf.Abs((int)scrollNum);

        targetItem = itemList[index % itemList.Count];

        if (currentItem != targetItem)
        {
            targetItem.Show();
            currentItem.Hide();

            currentItem = targetItem;
            SetTextValue();
        }
    }

    /// <summary>
    /// 鼠标滚轮操作---Material
    /// </summary>
    private void MouseScrollWheelMaterial()
    {
        scrollNum_Material += Input.GetAxis("Mouse ScrollWheel") * 5;
        index_Material = Mathf.Abs((int)scrollNum_Material);

        targetItem = itemList[index % itemList.Count];
        int tempIndex = 0;
        if (targetItem.materialList.Count ==0)
        {
            tempIndex = index_Material;
            isItemCtr = true;
        }
        else
        {
            tempIndex = index_Material % targetItem.materialList.Count;
            targetItem_Material = targetItem.materialList[tempIndex].GetComponent<MaterialItem>();
        }
            
        if (currentItem_Material != targetItem_Material )
        {
            tempBuildModel = m_BuildPanelView.MaterialModel[index % itemList.Count][index_Material % targetItem.materialList.Count];
            targetItem_Material.Height();
            if (currentItem_Material != null)
            {
                currentItem_Material.Normal();
            }
            currentItem_Material = targetItem_Material;
            SetTextValueMaterial();
        }
    }

    /// <summary>
    /// 设置Text内容---Item
    /// </summary>
    private void SetTextValue()
    {
        m_BuildPanelView.M_itemNane_Text.text = itemNames[index % itemNames.Length];
    }

    /// <summary>
    /// 设置Text内容---Material
    /// </summary>
    private void SetTextValueMaterial()
    {
        m_BuildPanelView.M_itemNane_Text.text = m_BuildPanelView.MaterialIconsName[index % itemList.Count][index_Material % targetItem.materialList.Count];
    }

    /// <summary>
    /// 使用射线确定建造模型的位置
    /// </summary>
    private void SetModelPosition()
    {

        if (BuildModel.name == "Ceiling_Light(Clone)" || BuildModel.name == "Roof(Clone)" || BuildModel.name == "Window(Clone)")
        {
            indexLayer = ~(1 << 13);
        }
        else
        {
            indexLayer = 1 << 0;
        }
        ray = m_BuildPanelView.M_EnvCamera.ScreenPointToRay(Input.mousePosition);



        if (Physics.Raycast(ray,out hit,15, indexLayer))
        {
                if (BuildModel.GetComponent<BuildModelBase>().IsAttach == false)
                {
                    if (BuildModel != null) BuildModel.GetComponent<Transform>().position = hit.point;
                }
                if (Vector3.Distance(hit.point, BuildModel.GetComponent<Transform>().position) > 3 )
                {
                    BuildModel.GetComponent<BuildModelBase>().IsAttach = false;
                }
        }
    }

    public void SetLeftKeyNull()
    {
        if (tempBuildModel != null)
        {
            tempBuildModel = null;
        }

        if (BuildModel != null)
        {
            GameObject.Destroy(BuildModel);
            BuildModel = null;
        }
        
    }
}
