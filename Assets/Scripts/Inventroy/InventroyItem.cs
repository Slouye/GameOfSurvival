using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyItem {
    int itemId;
    string itemName;
    int itemNum;
    int itemBar;

    public string ItemName
    {
        get
        {
            return itemName;
        }

        set
        {
            itemName = value;
        }
    }

    public int ItemNum
    {
        get
        {
            return itemNum;
        }

        set
        {
            itemNum = value;
        }
    }

    public int ItemId
    {
        get
        {
            return itemId;
        }

        set
        {
            itemId = value;
        }
    }

    public int ItemBar
    {
        get
        {
            return itemBar;
        }

        set
        {
            itemBar = value;
        }
    }

    public InventroyItem()
    {
    }

    public InventroyItem(int itemId, string itemName, int itemNum, int itemBar)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.itemNum = itemNum;
        this.itemBar = itemBar;
    }

    public override string ToString()
    {
        return string.Format("物品名称：{0}，数量{1},ID{2}",this.itemName,this.itemNum,this.itemId);
    }
}
