using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelView : MonoBehaviour {

    private Transform m_Transform;
    private Transform bg_Transform;
    private Transform models_Parent;
    private Transform player_Transform;

    private GameObject prefab_Item;
    private GameObject prefab_MaterialsBG;

    private Text itemNane_Text;
    private Camera EnvCamera;

    private List<Sprite> icons = new List<Sprite>();
    private List<Sprite[]> materialIcons = new List<Sprite[]>();
    private List<string[]> materialIconsName = new List<string[]>();
    private List<GameObject[]> materialModel = new List<GameObject[]>();


    public Transform M_Transform { get { return m_Transform; } }
    public Transform M_bg_Transform { get { return bg_Transform; } }
    public Transform M_models_Parent { get { return models_Parent; } }
    public Transform M_player_Transform { get { return player_Transform; } }

    public GameObject M_prefab_Item { get { return prefab_Item; } }
    public GameObject M_prefab_MaterialsBG { get{return prefab_MaterialsBG; } }

    public Text M_itemNane_Text { get { return itemNane_Text; } }
    public Camera M_EnvCamera { get { return EnvCamera; } }

    public List<Sprite> Icons { get { return icons; } }
    public List<Sprite[]> MaterialIcons { get { return materialIcons; } }
    public List<string[]> MaterialIconsName { get { return materialIconsName; } }
    public List<GameObject[]> MaterialModel { get { return materialModel; } }

    private void Awake()
    {
        Init();
        LoadIcons();
        LoadMaterialIcons();
        LoadMaterialIconsName();
        LoadMaterialModels();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        bg_Transform = m_Transform.Find("WheelBG");
        models_Parent = GameObject.Find("BuildModels").transform;
        prefab_Item = Resources.Load<GameObject>("BuildPanel/Prefab/Item");
        prefab_MaterialsBG = Resources.Load<GameObject>("BuildPanel/Prefab/MaterialsBG");
        itemNane_Text = m_Transform.Find("WheelBG/ItemName").GetComponent<Text>();
        player_Transform = GameObject.Find("FPSController").transform;
        EnvCamera = GameObject.Find("FPSController/PersonCamera/EnvCamera").GetComponent<Camera>();
    }


    /// <summary>
    /// 加载9个Icon图标
    /// </summary>
    private void LoadIcons()
    {
        icons.Add(null);
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Question Mark"));
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Roof_Category"));
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Stairs_Category"));
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Window_Category"));
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Door_Category"));
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Wall_Category"));
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Floor_Category"));
        icons.Add(Resources.Load<Sprite>("BuildPanel/Icon/Foundation_Category"));
    }

    /// <summary>
    /// 加载具体建造材料的图标
    /// </summary>
    private void LoadMaterialIcons()
    {
        materialIcons.Add(null);
        materialIcons.Add(new Sprite[] { LoadIcon("Ceiling Light"), LoadIcon("Pillar_Wood"), LoadIcon("Wooden Ladder") });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Roof_Metal"), null });
        materialIcons.Add(new Sprite[] { LoadIcon("Stairs_Wood"), LoadIcon("L Shaped Stairs_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Window_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Wooden Door"), null });
        materialIcons.Add(new Sprite[] { LoadIcon("Wall_Wood"), LoadIcon("Doorway_Wood"), LoadIcon("Window Frame_Wood") });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Floor_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Platform_Wood"), null });
    }

    /// <summary>
    /// 加载MaterialIcons对应的名字
    /// </summary>
    private void LoadMaterialIconsName()
    {
        materialIconsName.Add(null);
        materialIconsName.Add(new string[] { "吊灯", "木柱", "梯子" });
        materialIconsName.Add(new string[] { null, "屋顶", null });
        materialIconsName.Add(new string[] { "直梯", "L型梯", null });
        materialIconsName.Add(new string[] { null, "窗户", null });
        materialIconsName.Add(new string[] { null, "门", null });
        materialIconsName.Add(new string[] { "普通墙壁", "门型墙壁", "窗型墙壁" });
        materialIconsName.Add(new string[] { null, "地板", null });
        materialIconsName.Add(new string[] { null, "地基", null });
    }

    /// <summary>
    /// 加载Material模型
    /// </summary>
    private void LoadMaterialModels()
    {
        materialModel.Add(new GameObject[] { null });
        materialModel.Add(new GameObject[] { LoadBuildModel("Ceiling_Light"), LoadBuildModel("Pillar"), LoadBuildModel("Ladder") });
        materialModel.Add(new GameObject[] { null, LoadBuildModel("Roof"), null });
        materialModel.Add(new GameObject[] { LoadBuildModel("Stairs"), LoadBuildModel("L_Shaped_Stairs"), null });
        materialModel.Add(new GameObject[] { null, LoadBuildModel("Window"), null });
        materialModel.Add(new GameObject[] { null, LoadBuildModel("Door"), null });
        materialModel.Add(new GameObject[] { LoadBuildModel("Wall"), LoadBuildModel("Doorway"), LoadBuildModel("Window_Frame") });
        materialModel.Add(new GameObject[] { null, LoadBuildModel("Floor"), null });
        materialModel.Add(new GameObject[] { null, LoadBuildModel("Platform"), null });
    }

    /// <summary>
    /// 加载单个BuildModel
    /// </summary>
    private GameObject LoadBuildModel(string name)
    {
        return Resources.Load<GameObject>("BuildPanel/Prefabs/" + name);
    }

    /// <summary>
    /// 加载单个具体的图标
    /// </summary>
    private Sprite LoadIcon(string name)
    {
        return Resources.Load<Sprite>("BuildPanel/MaterialIcon/" + name);
    }

}
