
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
/// <summary>
/// 背包数据控制器
/// </summary>
public class InventroyPanelModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    /// <summary>
    /// 获得Json转化后的List集合
    /// </summary>
    /// <param name="fileName">json文件名</param>
    /// <returns>List集合</returns>
    public List<InventroyItem> GetJsonList(string fileName)
    {
        return JsonTools.LoadJsonFile<InventroyItem>(fileName);
    }
}
