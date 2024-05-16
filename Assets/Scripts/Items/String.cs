
/// <summary>
/// 거미줄(실) 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class String : Item
{
    public String(string name) : base(name) { this.itemNo = (int)ItemNo.STRING; this.imageName = "String"; }
}
