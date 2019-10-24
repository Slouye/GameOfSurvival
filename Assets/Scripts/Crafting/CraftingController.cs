using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingController:MonoBehaviour {
    private Transform m_Transform;
    private Transform bg_Transform;
    private Image m_Image;
    private Button m_Creaft_Button;
    private Button m_CreaftAll_Button;

    private GameObject prefab_InventroyItem;

    private int tempId;
    private string tempName;

    public GameObject Prefab_InventroyItem
    {
        set
        {
            prefab_InventroyItem = value;
        }
    }

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = m_Transform.Find("GoodItem/ItemImage").GetComponent<Image>();
        m_Creaft_Button = m_Transform.Find("Craft").GetComponent<Button>();
        m_CreaftAll_Button = m_Transform.Find("CraftAll").GetComponent<Button>();
        bg_Transform = m_Transform.Find("GoodItem").GetComponent<Transform>();

        m_Creaft_Button.onClick.AddListener(CraftingItem);
        m_Image.gameObject.SetActive(false);


        ButtonInit();
    }

    public void Init(int id,string fileName)
    {
        m_Image.gameObject.SetActive(true);
        m_Image.sprite = Resources.Load<Sprite>("Item/" + fileName);
        tempId = id;
        tempName = fileName;
    }

    private void ButtonInit()
    {
        m_Creaft_Button.interactable = false;
        m_Creaft_Button.transform.Find("Text").GetComponent<Text>().color = Color.black;
        m_Creaft_Button.transform.Find("Text").GetComponent<Outline>().effectColor = Color.black;

        m_CreaftAll_Button.interactable = false;
        m_CreaftAll_Button.transform.Find("Text").GetComponent<Text>().color = Color.black;
        m_CreaftAll_Button.transform.Find("Text").GetComponent<Outline>().effectColor = Color.black;
    }

    public void ActiveButton()
    {
        m_Creaft_Button.interactable = true;
        m_Creaft_Button.transform.Find("Text").GetComponent<Text>().color = Color.white;
        m_Creaft_Button.transform.Find("Text").GetComponent<Outline>().effectColor = new Color(78/255f,201/255f,114/255f,128/255f);
    }

    //物品合成
    private void CraftingItem()
    {
        Debug.Log("合成物品");
        GameObject item = GameObject.Instantiate<GameObject>(prefab_InventroyItem, bg_Transform);

        item.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);
        item.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
        item.GetComponent<InventroyItemController>().InitItem(tempName, 1, tempId,1);

        SendMessageUpwards("CraftingOK");

    }
}
