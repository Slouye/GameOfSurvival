using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingPanelTabItemController : MonoBehaviour {
    private Transform m_Transform;
    private Button m_Button;
    private GameObject m_ButtonBG;
    private Image m_Icon;

    private int index = -1;


    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Button = gameObject.GetComponent<Button>();
        m_ButtonBG = m_Transform.Find("Center_BG").gameObject;
        m_Icon = m_Transform.Find("Icon").GetComponent<Image>();

        m_Button.onClick.AddListener(ButtonOnClick);
    }

    /// <summary>
    /// 初始化化选项卡
    /// </summary>
    /// <param name="index"></param>
    public void InitItem(int index,Sprite sprite)
    {
        this.index = index;
        gameObject.name = "Tab" + index;
        m_Icon.sprite = sprite;
    }

    /// <summary>
    /// 选项卡默认状态
    /// </summary>
    public void NoramlTab()
    {
        if (m_ButtonBG.activeSelf == false)
        {
            m_ButtonBG.SetActive(true);
        }
    }

    /// <summary>
    /// 关闭选项卡激活状态
    /// </summary>
    public void ActiveTab()
    {
        m_ButtonBG.SetActive(false);
    }

    private void ButtonOnClick()
    {
        SendMessageUpwards("ResetTabsAndContents", index);
    }
}
