/// <summary>
/// 삽 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Shovel : Item
{
    public Shovel(string name) : base(name) { this.itemNo = (int)ItemNo.SHOVEL; this.imageName = "Shovel"; }
}