using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanel : MonoBehaviour {
    private Transform m_Transform;
    private Image hp_Bar;
    private Image vit_Bar;
    
    void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        hp_Bar = m_Transform.Find("HP/Bar").GetComponent<Image>();
        vit_Bar = m_Transform.Find("VIT/Bar").GetComponent<Image>();

    }
	
    /// <summary>
    /// 设置血量条
    /// </summary>
	public void SetHPBar(int value)
    {
        hp_Bar.fillAmount = value * 0.001f;
    }

    public void SetVITBar(int value)
    {
        vit_Bar.fillAmount = value * 0.01f;
    }
	
}
