using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftingContent : MonoBehaviour {
    private Transform m_Transform;
    
    private int index = -1;
    CraftingContentItemController current = null;
    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
       

    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="index"></param>
    /// <param name="prefab"></param>
    /// <param name="list"></param>
    public void InitContent(int index, GameObject prefab,List<CraftingContentItem> list)
    {
        this.index = index;
        gameObject.name = "Content" + index;
        CreateAllItems(prefab, list);
    }

    /// <summary>
    /// 创建正文元素
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="list"></param>
    private void CreateAllItems(GameObject prefab , List<CraftingContentItem> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(prefab, m_Transform);
            go.GetComponent<CraftingContentItemController>().Init(list[i]);
        }
    }

    /// <summary>
    /// 正文选中状态切换
    /// </summary>
    /// <param name="item"></param>
    private void ResetItemState(CraftingContentItemController item)
    {
        //Debug.Log(item.Id);
        if (item == current)
        {
            return;
        }
        if (current != null)
        {
            current.NormalItem();
        }
        item.ActiveItem();
        current = item;
        SendMessageUpwards("CreateSlotContents", item.Id);
    }
}
