/// <summary>
/// 철 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Iron : Item
{
    public Iron(string name) : base(name) 
    { this.itemNo = (int)ItemNo.IRON;
        this.imageName = "Iron"; }
}