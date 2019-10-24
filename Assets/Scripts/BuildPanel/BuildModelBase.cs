using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildModelBase : MonoBehaviour {
    protected bool isCunPut = false;           //当前模型位置是否可以摆放
    private bool isAttach = false;          //当前模型的吸附状态
    private Material newMaterial;           //透明材质球
    private Material oldMaterial;           //原始材质球

    public bool IsCunPut
    {
        get
        {
            return isCunPut;
        }
        set
        {
            isCunPut = value;
        }
    }

    public bool IsAttach
    {
        get
        {
            return isAttach;
        }

        set
        {
            isAttach = value;
        }
    }

    protected virtual void Awake()
    {
        newMaterial = Resources.Load<Material>("BuildPanel/Building Preview");
        oldMaterial = gameObject.GetComponent<MeshRenderer>().material;
        gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    private void Update()
    {
        if (IsCunPut)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0, 255, 0, 100);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color32(255, 0, 0, 100);
        }
    }

    public virtual void Normal()
    {
        gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
    }


   
    protected abstract void OnTriggerEnter(Collider other);
    protected abstract void OnTriggerExit(Collider other);
}
