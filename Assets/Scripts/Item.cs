using System.Collections.Generic;
/// <summary>
/// ������ �⺻ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Item
{
    public string name { get; }

    /// <summary> ���⿡ �������� �̹������� �̸��� ������ �˴ϴ�.</summary>
    public string imageName;
    /// <summary> �����۸��� �ο��Ǵ� ������ȣ�Դϴ�. (int)ItemNo �� �߰��Ͽ� �̿��ϸ� �˴ϴ�.</summary>
    public int itemNo;

    public Item(string name)
    {
        this.name = name;
        this.itemNo = (int)ItemNo.NONE;
    }
}

/// <summary>
/// �����ۿ� �ο��Ǵ� ������ȣ enum �Դϴ�.
/// </summary>
[System.Serializable]
enum ItemNo
{
    NONE ,STICK, IRON, STRING, PIXAXE, CARROT, AXE, SWORD, BOW, SHOVEL, HOE, FISHINGROD, CARROTFISHINGROD
};

/// <summary>
/// ���Ŀ� ������������ �����ϱ� ���� ����Ʈ Ŭ�����Դϴ�.
/// </summary>
[System.Serializable]
public class ItemList
{
    public List<Item> items = new List<Item>();
}