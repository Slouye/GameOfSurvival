using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMapItem {
    private int mapId;
    private string[] mapContents;
    private int materialsCount;
    private string mapName;

    public int MapId
    {
        get
        {
            return mapId;
        }

        set
        {
            mapId = value;
        }
    }

    public string[] MapContents
    {
        get
        {
            return mapContents;
        }

        set
        {
            mapContents = value;
        }
    }

    public string MapName
    {
        get
        {
            return mapName;
        }

        set
        {
            mapName = value;
        }
    }

    public int MaterialsCount
    {
        get
        {
            return materialsCount;
        }

        set
        {
            materialsCount = value;
        }
    }

    public CraftingMapItem(int mapId, string[] mapContents, int materialsCount, string mapName)
    {
        this.mapId = mapId;
        this.mapContents = mapContents;
        this.materialsCount = materialsCount;
        this.mapName = mapName;
    }

    public override string ToString()
    {
        return string.Format("id{0},map{1},name{2},count",this.mapId,this.mapContents.Length,this.mapName,this.materialsCount);
    }
}
