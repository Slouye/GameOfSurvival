using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolBarSlotController : MonoBehaviour {


    private Transform m_Transform;
    private Button m_Button;
    private Image m_Image;

    private Text m_Text;

    private bool selfState = false;     //false：为选中，true：选中

    public bool SelfState { get { return selfState; } }

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Button = gameObject.GetComponent<Button>();
        m_Image = gameObject.GetComponent<Image>();

        m_Text = m_Transform.Find("Key").GetComponent<Text>();

        m_Button.onClick.AddListener(SlotClick);

    }

    public void Init(string name,int keyNum)
    {
        gameObject.name = name;
        m_Text.text = keyNum.ToString();
    }

    public void SlotClick()
    {
        //如果是激活状态则恢复为为激活状态
        if (SelfState)
        {
            Normal();
        }
        else
        {
            Active();
        }

        SendMessageUpwards("SaveActiveSlot",gameObject);
    }

    public void Normal()
    {
        m_Image.color = Color.white;
        selfState = false;
    }

    private void Active()
    {
        m_Image.color = Color.red;
        selfState = true;
    }
}
