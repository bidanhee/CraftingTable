/// <summary>
/// ���˴� ������ Ŭ���� �Դϴ�.
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