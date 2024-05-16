/// <summary>
/// 활 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class Bow : Item
{
    public Bow(string name)
        : base(name)
    {
        this.itemNo = (int)ItemNo.BOW;
        this.imageName = "Bow";
    }
}