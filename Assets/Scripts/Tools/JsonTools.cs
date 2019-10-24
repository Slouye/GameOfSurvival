using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public sealed class JsonTools {

    /// <summary>
    /// 通过文件名加载Json
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<T> LoadJsonFile<T>(string fileName)
    {
        List<T> tempList = new List<T>();
        string tempJsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;
        
        JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            T ii = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            tempList.Add(ii);
        }
        return tempList;
    }
}
