/// <summary>
/// �� ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Shovel : Item
{
    public Shovel(string name) : base(name) { this.itemNo = (int)ItemNo.SHOVEL; this.imageName = "Shovel"; }
}