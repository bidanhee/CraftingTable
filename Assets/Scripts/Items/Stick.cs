using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 막대기 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Stick : Item
{
    public Stick(string name, string imageName)
        : base(name, imageName)
    {
        this.itemNo = (int)ItemNo.STICK;
    }
}