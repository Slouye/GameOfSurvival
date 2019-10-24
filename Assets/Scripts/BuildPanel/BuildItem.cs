
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem {
    private string modelName;
    private string posX;
    private string posY;
    private string posZ;

    private string rotX;
    private string rotY;
    private string rotZ;
    private string rotW;

    

    

    public BuildItem()
    {
    }

    public string PosX
    {
        get
        {
            return posX;
        }

        set
        {
            posX = value;
        }
    }

    public string PosY
    {
        get
        {
            return posY;
        }

        set
        {
            posY = value;
        }
    }

    public string PosZ
    {
        get
        {
            return posZ;
        }

        set
        {
            posZ = value;
        }
    }

    public string RotX
    {
        get
        {
            return rotX;
        }

        set
        {
            rotX = value;
        }
    }

    public string RotY
    {
        get
        {
            return rotY;
        }

        set
        {
            rotY = value;
        }
    }

    public string RotZ
    {
        get
        {
            return rotZ;
        }

        set
        {
            rotZ = value;
        }
    }

    public string RotW
    {
        get
        {
            return rotW;
        }

        set
        {
            rotW = value;
        }
    }

    public string ModelName
    {
        get
        {
            return modelName;
        }

        set
        {
            modelName = value;
        }
    }

    public BuildItem(string modelName, string posX, string posY, string posZ, string rotX, string rotY, string rotZ, string rotW)
    {
        this.ModelName = modelName;
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        this.rotX = rotX;
        this.rotY = rotY;
        this.rotZ = rotZ;
        this.rotW = rotW;
    }
}
