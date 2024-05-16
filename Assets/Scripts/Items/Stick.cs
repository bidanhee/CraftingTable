/// <summary>
/// 막대기 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Stick : Item
{
    public Stick(string name) : base(name) { this.itemNo = (int)ItemNo.STICK; this.imageName = "Stick"; }
}