/// <summary>
/// 도끼 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Axe : Item
{
    public Axe(string name)
        : base(name)
    {
        this.itemNo = (int)ItemNo.AXE;
        this.imageName = "Axe";
    }
}