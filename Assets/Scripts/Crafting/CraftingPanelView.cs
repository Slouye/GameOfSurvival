using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanelView : MonoBehaviour {
    private Transform m_Transform;
    private Transform tabs_Transform;
    private Transform contents_Transform;
    private Transform center_Transform;

    private GameObject prefab_TabsItem;
    private GameObject prefab_CraftingContent;
    private GameObject prefab_CraftingContentItem;
    private GameObject prefab_Slot;
    private GameObject prefab_InventroyItem;


    public Transform Transform { get{ return m_Transform; } }
    public Transform Tabs_Transform { get{ return tabs_Transform; } }
    public Transform Contents_Transform { get{ return contents_Transform; } }
    public Transform Center_Transform { get{ return center_Transform; } }

    public GameObject Tabs_Item { get{ return prefab_TabsItem; } }
    public GameObject Crafting_Content { get{ return prefab_CraftingContent; } }
    public GameObject Crafting_ContentItem { get{ return prefab_CraftingContentItem; } }
    public GameObject Crafting_Slot { get{ return prefab_Slot; } }
    public GameObject Prefab_InventroyItem { get{ return prefab_InventroyItem; } }

    private Dictionary<string, Sprite> tabIconDic;
    private Dictionary<string, Sprite> materialIconDic;

    private void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        tabs_Transform = m_Transform.Find("Left/Tabs");
        contents_Transform = m_Transform.Find("Left/Contents");
        center_Transform = m_Transform.Find("Center");

        prefab_TabsItem = Resources.Load<GameObject>("CraftingTabItem");
        prefab_CraftingContent = Resources.Load<GameObject>("CraftingContent");
        prefab_CraftingContentItem = Resources.Load<GameObject>("CraftingContentItem");
        prefab_Slot = Resources.Load<GameObject>("CraftinSlot");
        prefab_InventroyItem = Resources.Load<GameObject>("InventroyItem");

        tabIconDic = new Dictionary<string, Sprite>();
        materialIconDic = new Dictionary<string, Sprite>();

        //加载选项卡图标
        tabIconDic = ResourcesTools.LoadFolderAssets("TabIcon", tabIconDic);

        //加载所有材料图标
        materialIconDic = ResourcesTools.LoadFolderAssets("Material", materialIconDic);
        
    }

    /// <summary>
    /// 根据名称得到选项卡图标
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Sprite ByNameGetSprites(string name)
    {
        return ResourcesTools.GetAsset(name, tabIconDic);
    }

    /// <summary>
    /// 根据名称得到材料图片
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Sprite ByNameGetMaterialIconSprites(string name)
    {
        return ResourcesTools.GetAsset(name, materialIconDic);
    }
}
