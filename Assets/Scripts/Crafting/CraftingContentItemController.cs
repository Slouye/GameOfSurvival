using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingContentItemController : MonoBehaviour {
    private Transform m_Transform;
    private Text m_Text;
    private GameObject m_BG;
    private Button m_Button;
    private int id;
    private string name;

    public int Id
    {
        get
        {
            return id;
        }
    }

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Text = m_Transform.Find("Text").GetComponent<Text>();
        m_Button = gameObject.GetComponent<Button>();
        m_BG = m_Transform.Find("BG").gameObject;

        m_Button.onClick.AddListener(ItemButtonClick);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="name"></param>
    public void Init(CraftingContentItem item)
    {
        id = item.ItemID;
        name = item.ItemName;
        m_Text.text = name;
        gameObject.name = "item" + id;
    }

    /// <summary>
    /// 默认状态
    /// </summary>
    public void NormalItem()
    {
        m_BG.SetActive(false);
    }

    /// <summary>
    /// 选中状态
    /// </summary>
    public void ActiveItem()
    {
        m_BG.SetActive(true);
    }

    /// <summary>
    /// 按钮点击
    /// </summary>
    public void ItemButtonClick()
    {
        SendMessageUpwards("ResetItemState", this);
    }
}
