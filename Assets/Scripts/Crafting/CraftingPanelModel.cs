using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class CraftingPanelModel : MonoBehaviour {
    Dictionary<int, CraftingMapItem> mapItemDic;

    private void Awake()
    {
        mapItemDic = LoadMapContents("CraftingMapJsonData");
    }
    /// <summary>
    /// 获取选项卡图片名称
    /// </summary>
    /// <returns></returns>
    public string[] GetTabsIconName()
    {
        return new string[] { "Icon_House", "Icon_Weapon" };
    }

    /// <summary>
    /// 获取正文信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<List<CraftingContentItem>> ByNameGetJsonData(string name)
    {
        List<List<CraftingContentItem>> tempList = new List<List<CraftingContentItem>>();
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + name).text;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            List<CraftingContentItem> temp = new List<CraftingContentItem>();
            JsonData jd = jsonData[i]["Type"];
            for (int j = 0; j < jd.Count; j++)
            {
                temp.Add(JsonMapper.ToObject<CraftingContentItem>(jd[j].ToJson()));
            }
            tempList.Add(temp);
        }

        return tempList;
    }

    /// <summary>
    /// 获取合成图谱
    /// </summary>
    /// <returns></returns>
    private Dictionary<int,CraftingMapItem> LoadMapContents(string name)
    {
        Dictionary<int, CraftingMapItem> temp = new Dictionary<int, CraftingMapItem>();
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + name).text;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            int mapId = int.Parse(jsonData[i]["MapId"].ToString());
            string tempStr = jsonData[i]["MapContents"].ToString();
            string[] mapStr = tempStr.Split(',');
            int mapCount = int.Parse(jsonData[i]["MaterialsCount"].ToString());
            string mapName = jsonData[i]["MapName"].ToString();
            CraftingMapItem item = new CraftingMapItem(mapId, mapStr, mapCount, mapName);
            temp.Add(mapId, item);
        }
        return temp;
    }

    /// <summary>
    /// 根据ID获得对应图谱数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CraftingMapItem ByIdGetMapItem(int id)
    {
        CraftingMapItem temp = null;
        mapItemDic.TryGetValue(id,out temp);
        return temp;
    }
}
