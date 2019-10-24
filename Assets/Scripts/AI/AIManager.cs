using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIManagerType
{
    CANNIBAL,
    BOAR,
    NULL
}

public enum AIType
{
    CANNIBAL,
    BOAR,
    NULL
}

public class AIManager : MonoBehaviour {
    private Transform m_Transform;
    private GameObject prefab_Cannibal;
    private GameObject prefab_Boar;
    private AIManagerType aiManagerType = AIManagerType.NULL;
    private List<GameObject> aiList = new List<GameObject>();
    private Transform[] posTransform;
    private List<Vector3> posList = new List<Vector3>();

    private int index = 0;

    public AIManagerType AiManagerType { get { return aiManagerType; } set { aiManagerType = value; } }

    void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        prefab_Cannibal = Resources.Load<GameObject>("AI/Cannibal");
        prefab_Boar = Resources.Load<GameObject>("AI/Boar");

        GameObject.Find("FPSController").GetComponent<PlayerController>().PlayerDeathDel += Death;

        //加上true 只查找隐藏的
        posTransform = m_Transform.GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < posTransform.Length; i++)
        {
            posList.Add(posTransform[i].position);
        }

       

        CreateAIByEnum();
    }
	
    private void CreateAIByEnum()
    {
        if (aiManagerType == AIManagerType.CANNIBAL)
        {
            CreateAI(prefab_Cannibal,AIType.CANNIBAL);
        }
        else if (aiManagerType == AIManagerType.BOAR)
        {
            CreateAI(prefab_Boar,AIType.BOAR);
        }
    }

    private void CreateAI(GameObject prefab_AI,AIType aIType)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject ai = GameObject.Instantiate<GameObject>(prefab_AI, m_Transform.position, Quaternion.identity, m_Transform);
            ai.GetComponent<AI>().Dir = posList[i];
            ai.GetComponent<AI>().PosList = posList;
            ai.GetComponent<AI>().Life = 300;
            ai.GetComponent<AI>().Attack = 100;
            ai.GetComponent<AI>().AIType = aIType;
           aiList.Add(ai);
        }
    }
	
    private void AIDeath(GameObject ai)
    {
        aiList.Remove(ai);
        StartCoroutine(CreateOneAI());
    }

    IEnumerator CreateOneAI()
    {
        yield return new WaitForSeconds(3);
        GameObject ai = null;
        if (aiManagerType == AIManagerType.CANNIBAL)
        {
            ai = GameObject.Instantiate<GameObject>(prefab_Cannibal, m_Transform.position, Quaternion.identity, m_Transform);
            ai.GetComponent<AI>().AIType = AIType.CANNIBAL;
        }
        else if (aiManagerType == AIManagerType.BOAR)
        {
            ai = GameObject.Instantiate<GameObject>(prefab_Boar, m_Transform.position, Quaternion.identity, m_Transform);
            ai.GetComponent<AI>().AIType = AIType.BOAR;
        }
        //ai.GetComponent<AI>().M_AIState = AIState.IDLE;
        ai.GetComponent<AI>().Dir = posList[index];
        ai.GetComponent<AI>().PosList = posList;
        ai.GetComponent<AI>().Life = 300;
        ai.GetComponent<AI>().Attack = 100;

        index++;
        index = index % posList.Count;

        aiList.Add(ai);
    }

    /// <summary>
    /// 玩家死亡，ai角色销毁
    /// </summary>
    private void Death()
    {
        for (int i = 0; i < aiList.Count; i++)
        {
            GameObject.Destroy(aiList[i]);
        }
    }
}
