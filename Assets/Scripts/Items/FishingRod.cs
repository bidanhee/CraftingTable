/// <summary>
/// ���˴� ������ Ŭ���� �Դϴ�.
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