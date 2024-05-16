/// <summary>
/// 낚싯대 아이템 클래스 입니다.
/// </summary>
[System.Serializable]
public class CarrotFishingRod : Item
{
    public CarrotFishingRod(string name) : base(name)
    {
        this.itemNo = (int)ItemNo.CARROTFISHINGROD;
        this.imageName = "CarrotOnFishingRod";
    }
}