using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 아이템 기본 클래스 입니다.
/// </summary>
[System.Serializable]
public class Item
{

    public string name { get; }
    public string imageName { get; }
    public int itemNo;
    public Item(string name, string imageName)
    {
        this.name = name;
        this.imageName = imageName;
        this.itemNo = (int)ItemNo.NONE;
    }
}

[System.Serializable]
enum ItemNo
{
    NONE ,STICK, IRON, STRING, PIXAXE, CARROT
};

[System.Serializable]
public class ItemList
{
    public List<Item> items = new List<Item>();
}