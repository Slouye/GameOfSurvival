using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingSlotController : MonoBehaviour {
    private Transform m_Transform;
    private Image m_Image;
    private bool isOpen = false;
    [SerializeField]private int id;
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = m_Transform.Find("Icon").GetComponent<Image>();
        m_Image.gameObject.SetActive(false);
    }

    public void Init(Sprite sprite,string id)
    {
 
        m_Image.gameObject.SetActive(true);
        m_Image.sprite = sprite;
        //m_Image.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
        m_Image.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        isOpen = true;
        this.id = int.Parse(id);
    }

    public void Reset()
    {
        m_Image.gameObject.SetActive(false);
    }
}
