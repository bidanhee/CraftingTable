/// <summary>
/// 검 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Sword : Item
{
    public Sword(string name)
        : base(name)
    {
        this.itemNo = (int)ItemNo.SWORD;
        this.imageName = "Sword";
    }
}