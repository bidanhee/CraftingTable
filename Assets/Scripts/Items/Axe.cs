/// <summary>
/// ���� ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Axe : Item
{
    public Axe(string name)
        : base(name)
    {
        this.itemNo = (int)ItemNo.AXE;
        this.imageName = "Axe";
    }
}