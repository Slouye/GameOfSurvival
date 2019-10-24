using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventroyItemController : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler{

    RectTransform m_RectTransform;
    CanvasGroup m_CanvasGroup;

    Image m_Image;                  //物品图标
    Image m_Bar;                    //耐久血条
    Text m_Text;                    //物品ui数量
    Transform parent;               //物品拖拽过程中临时的父物品
    Transform self_Parent;          //物品自身的父物体
    int id;                         //自身ID
    int num = 0;                    //物品数量
    public bool inInentroy = true;  //true为在背包内，flas为在合成槽内
    bool isDrag = false;            //是否为拖拽状态中
    int bar = 0 ;                   //耐久血条值 0:不需要  1：需要

    bool isSplit = true;            //是否可拆分

    public GameObject target;

    public int Num
    {
        get { return num;}
        set {
            num = value;
            m_Text.text = num.ToString();
        }
    }

    public int Id { get { return id; } set { id = value; } }

    public bool InInentroy
    {
        get { return inInentroy;}
        set {
            inInentroy = value;
            m_RectTransform.localPosition = Vector3.zero;
            ResetSpriteSize(m_RectTransform, 85, 85);
        }
    }

    private void Awake()
    {
        FindInit();
    }

    private void Update()
    {
        //物品拖拽状态中，按下鼠标右键拆分材料
        if (Input.GetMouseButtonDown(1) && isDrag)
        {
            if (m_RectTransform.Find("Bar").gameObject.activeSelf == false)
            {
                if (target != null)
                {
                    if (target.tag == "InventroySlot" || target.tag == "CraftinSlot")
                    {
                        if (num >1)
                        {
                            if (isSplit)
                            {
                                BreakMaterials();
                                isSplit = false;
                            }
                            
                        }
                       
                    }
                }
            }

        }

    }

    //查找相关的初始化
    private void FindInit()
    {
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        m_Image = gameObject.GetComponent<Image>();
        m_Bar = m_RectTransform.Find("Bar").GetComponent<Image>();
        m_Text = m_RectTransform.Find("Num").GetComponent<Text>();
        m_RectTransform.gameObject.name = "InventroyItem";
        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
        //parent = m_RectTransform.parent.parent.parent.parent;
        parent = GameObject.Find("Canvas").GetComponent<Transform>();
        
    }

   
    public void BarUIUpDate(float value)
    {
        m_Bar.fillAmount = value;
        if (value <= 0)
        {
            gameObject.transform.parent.GetComponent<ToolBarSlotController>().Normal();
            GameObject.Destroy(gameObject);
        }
    }

    public void InitItem(string itemName,int num,int id,int bar)
    {
        m_Image.sprite = Resources.Load<Sprite>("Item/" + itemName);
        m_Text.text = num.ToString();
        this.Id = id;
        this.num = num;
        this.bar = bar;
        BarOrNum();
    }

    //是否显示耐久血条
    private void BarOrNum()
    {
        if (bar == 0)
        {
            m_Bar.gameObject.SetActive(false);
        }
        else
        {
            m_Text.gameObject.SetActive(false);
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        self_Parent = m_RectTransform.parent;
        m_RectTransform.SetParent(parent);
        //设置射线不检测
        m_CanvasGroup.blocksRaycasts = false;
        isDrag = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position, eventData.enterEventCamera, out pos);
        m_RectTransform.position = pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        target = eventData.pointerEnter;
        //正常拖拽
        ItemDrag(target);

        //设置射线检测
        m_CanvasGroup.blocksRaycasts = true;
        //m_RectTransform.parent = target.GetComponent<Transform>();
        m_RectTransform.localPosition = Vector3.zero;
    }

    //重置图标大小
    private void ResetSpriteSize(RectTransform rectTransform,float width,float height)
    {
        //设置图片宽高
        //SetSizeWithCurrentAnchors(RectTransform.Axis.横或纵, 长度)
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

    }

    /// <summary>
    /// 材料拆分
    /// </summary>
    private void BreakMaterials()
    {
        //复制材料A 为 材料B
        GameObject tempB = GameObject.Instantiate<GameObject>(gameObject);
        RectTransform tempTransform = tempB.GetComponent<RectTransform>();
        //重置材料B属性
        tempTransform.parent = self_Parent;
        tempTransform.localPosition = Vector3.zero;
        tempTransform.localScale = Vector3.one;
        //数量拆分
        int tempCount = num;
        int tempNumB = tempCount / 2;
        int tempNumA = tempCount - tempNumB;
        //数量更新
        tempB.GetComponent<InventroyItemController>().Num = tempNumB;
        Num = tempNumA;
        //恢复射线检查
        tempB.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //id
        tempB.GetComponent<InventroyItemController>().Id = id;

    }


    private void MergeMaterials(InventroyItemController target)
    {
        target.Num = num + target.Num;
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// 物品拖拽逻辑
    /// </summary>
    private void ItemDrag(GameObject target)
    {
        if (target != null)
        {
            #region 空物品槽&&非空物品槽
            //拖拽是否为物品槽
            if (target.tag == "InventroySlot")
            {
                m_RectTransform.parent = target.GetComponent<Transform>();
                ResetSpriteSize(m_RectTransform, 85, 85);
                inInentroy = true;
            }
            else
            {
                m_RectTransform.parent = self_Parent.GetComponent<Transform>();
            }
            #endregion

            #region 交换位置
            if (target.tag == "InventroyItem")
            {
                InventroyItemController iic = target.GetComponent<InventroyItemController>();
                //判断是否在背包内
                if (inInentroy && iic.InInentroy)
                {
                    //判断id是否相同 相同合并
                    if (id == iic.Id)
                    {
                        MergeMaterials(iic);
                    }
                    else   //不相同交换位置
                    {
                        if (target.transform.parent.name != "GoodItem")
                        {
                            Transform targetTransform = target.GetComponent<Transform>();
                            m_RectTransform.SetParent(targetTransform.parent);
                            targetTransform.SetParent(self_Parent);
                            targetTransform.localPosition = Vector3.zero;
                        }
                        
                    }
                }
                else //不在背包内
                {
                    //同id合并
                    if (id == iic.Id)
                    {
                        MergeMaterials(iic);
                    }
                }
            }
            #endregion

            #region 拖拽到图谱
            if (target.tag == "CraftinSlot")
            {
                //检查是否为材料槽
                if (target.GetComponent<CraftingSlotController>().IsOpen)
                {
                    //检查是否为对应的材料
                    if (Id == target.GetComponent<CraftingSlotController>().Id)
                    {
                        m_RectTransform.parent = target.GetComponent<Transform>();
                        ResetSpriteSize(m_RectTransform, 60, 55);
                        inInentroy = false;
                        InventroyPanelController.Instance.SendDargMaterialsItem(gameObject);
                    }
                    else
                    {
                        m_RectTransform.parent = self_Parent.GetComponent<Transform>();
                    }

                }
                else
                {
                    m_RectTransform.parent = self_Parent.GetComponent<Transform>();
                }

            }
            #endregion
            isSplit = true;
        }
        else  //拖拽到非UI位置
        {
            m_RectTransform.parent = self_Parent.GetComponent<Transform>();
        }
    }
}
