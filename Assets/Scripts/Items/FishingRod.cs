/// <summary>
/// 낚싯대 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class FishingRod : Item
{
    public FishingRod(string name) : base(name)
    {
        this.itemNo = (int)ItemNo.FISHINGROD;
        this.imageName = "FishingRod";
    }
}