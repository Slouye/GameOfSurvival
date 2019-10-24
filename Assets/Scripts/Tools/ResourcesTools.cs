using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ResourcesTools {

    /// <summary>
    /// 加载文件夹资源
    /// </summary>
    /// <param name="folderName"></param>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static Dictionary<string,Sprite> LoadFolderAssets(string folderName,Dictionary<string,Sprite> dic)
    {
        Sprite[] tempSprites = Resources.LoadAll<Sprite>(folderName);
        for (int i = 0; i < tempSprites.Length; i++)
        {
            dic.Add(tempSprites[i].name, tempSprites[i]);
        }
        return dic;
    }

    /// <summary>
    /// 通过名字获取资源
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static Sprite GetAsset(string name, Dictionary<string,Sprite> dic)
    {
        Sprite temp = null;
        dic.TryGetValue(name, out temp);
        return temp;
    }
}
