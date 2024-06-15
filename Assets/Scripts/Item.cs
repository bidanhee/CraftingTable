using System.Collections.Generic;
/// <summary>
/// 아이템 기본 클래스 입니다.
/// </summary>
[System.Serializable]
public class Item
{
    public string name { get; }

    /// <summary> 여기에 아이템의 이미지파일 이름을 넣으면 됩니다.</summary>
    public string imageName;
    /// <summary> 아이템마다 부여되는 고유번호입니다. (int)ItemNo 를 추가하여 이용하면 됩니다.</summary>
    public int itemNo;

    public Item(string name)
    {
        this.name = name;
        this.itemNo = (int)ItemNo.NONE;
    }
}

/// <summary>
/// 아이템에 부여되는 고유번호 enum 입니다.
/// </summary>
[System.Serializable]
enum ItemNo
{
    NONE ,STICK, IRON, STRING, PIXAXE, CARROT, AXE, SWORD, BOW, SHOVEL, HOE, FISHINGROD, CARROTFISHINGROD
};

/// <summary>
/// 추후에 아이템정보를 저장하기 위한 리스트 클래스입니다.
/// </summary>
[System.Serializable]
public class ItemList
{
    public List<Item> items = new List<Item>();
}