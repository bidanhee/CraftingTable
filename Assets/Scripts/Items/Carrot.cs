/// <summary>
/// 당근 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Carrot : Item
{
    public Carrot(string name) : base(name)
    {
        this.itemNo = (int)ItemNo.CARROT;
        this.imageName = "Carrot";
    }
}