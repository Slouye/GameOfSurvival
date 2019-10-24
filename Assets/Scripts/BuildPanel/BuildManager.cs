using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;

public class BuildManager : MonoBehaviour {
    private Transform m_Transform;
    private List<Transform> allModlesTransform;   //所有的Model的Transform组件.
    private List<BuildItem> modelsList;        //Model模型的集合.
    private List<BuildItem> posList;         //从Json文本中获取到的.

    private string jsonPath = null;         //json文本文件的路径地址.
    private GameObject prefab_Cube;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        modelsList = new List<BuildItem>();
        posList = new List<BuildItem>();
        allModlesTransform = new List<Transform>();
        jsonPath = Application.dataPath + @"\Resources\json.txt";
        JsonToObject();
    }


    
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (BuildPanelController.Instance.BuildModel == null)
            {
                ObjectToJson();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            
            JsonToObject();
        }

    }

    /// <summary>
    /// 对象转换为Json数据.
    /// </summary>
    private void ObjectToJson()
    {
       
        modelsList.Clear();
        //allModlesTransform = GameObject.Find("BuildModels").GetComponentsInChildren<Transform>();
        int nub = GameObject.Find("BuildModels").transform.childCount;
        for (int i = 0; i < nub; i++)
        {
            allModlesTransform.Add(GameObject.Find("BuildModels").transform.GetChild(i));
        }

        for (int i = 0; i < allModlesTransform.Count; i++)
        {
            Vector3 pos = allModlesTransform[i].position;
            Quaternion rot = allModlesTransform[i].rotation;
            BuildItem item = new BuildItem(allModlesTransform[i].name,Math.Round(pos.x,2).ToString(), Math.Round(pos.y,2).ToString(), Math.Round(pos.z,2).ToString(), Math.Round(rot.x,2).ToString(), Math.Round(rot.y,2).ToString(), Math.Round(rot.z,2).ToString(), Math.Round(rot.w,2).ToString());
            modelsList.Add(item);
        }

        string str = JsonMapper.ToJson(modelsList);
        Debug.Log(str);

        File.Delete(jsonPath);
        StreamWriter sw = new StreamWriter(jsonPath);
        sw.Write(str);
        sw.Close();
    }


    /// <summary>
    /// JSON转换为多个对象.
    /// </summary>
    private void JsonToObject()
    {
        //TextAsset textAsset = Resources.Load<TextAsset>("json");
        String textAsset = File.ReadAllText(jsonPath);

        Debug.Log(textAsset);

        JsonData jsonData = JsonMapper.ToObject(textAsset);
        for (int i = 0; i < jsonData.Count; i++)
        {
            //Debug.Log(jsonData[i].ToJson());
            BuildItem item = JsonMapper.ToObject<BuildItem>(jsonData[i].ToJson());
            posList.Add(item);
        }

        for (int i = 0; i < posList.Count; i++)
        {
            //Vector3 pos = new Vector3(float.Parse(posList[i].PosX), float.Parse(posList[i].PosY), float.Parse(posList[i].PosZ));
            Vector3 pos = new Vector3(float.Parse(posList[i].PosX), float.Parse(posList[i].PosY), float.Parse(posList[i].PosZ));
            Quaternion rot = new Quaternion(float.Parse(posList[i].RotX), float.Parse(posList[i].RotY), float.Parse(posList[i].RotZ), float.Parse(posList[i].RotW));
            prefab_Cube = Resources.Load<GameObject>(@"BuildPanel/Prefabs/" + posList[i].ModelName);
            GameObject go = GameObject.Instantiate<GameObject>(prefab_Cube, pos, rot, GameObject.Find("BuildModels").transform);
            go.name = posList[i].ModelName;
            go.layer = 14;
            go.GetComponent<BuildModelBase>().Normal();
            GameObject.Destroy(go.GetComponent<BuildModelBase>());
        }

    }

}
