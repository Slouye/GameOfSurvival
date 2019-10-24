using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    private Image icon_Image;
    private Image bg_Image;

    public List<GameObject> materialList = new List<GameObject>();

    
    private void Awake()
    {
        icon_Image = gameObject.GetComponent<Transform>().Find("Icon").GetComponent<Image>();
        bg_Image = gameObject.GetComponent<Image>();

      
    }

    public void Init(string name,Quaternion quaternion,bool isIcon, Sprite sprite, bool isShow)
    {
        gameObject.name = name;
        gameObject.transform.rotation = quaternion;
        gameObject.transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);
        icon_Image.enabled = isIcon;
        icon_Image.GetComponent<Image>().sprite = sprite;
        bg_Image.enabled = isShow;
    }

    /// <summary>
    /// 显示扇形背景
    /// </summary>
    public void Show()
    {
        bg_Image.enabled = true;
        ShowAndHide(true);
    }

    /// <summary>
    /// 隐藏扇形背景
    /// </summary>
    public void Hide()
    {
        bg_Image.enabled = false;
        ShowAndHide(false);
    }

    public void MaterialListAdd(GameObject material)
    {
        materialList.Add(material);
    }

    /// <summary>
    /// 显示隐藏建造图标
    /// </summary>
    private void ShowAndHide(bool flag)
    {
        if (materialList == null)
        {
            return;
        }
        for (int i = 0; i < materialList.Count; i++)
        {
            materialList[i].SetActive(flag);
        }
    }


}
