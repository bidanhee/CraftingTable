
/// <summary>
/// ���(ö) ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Pickaxe : Item
{
    public Pickaxe(string name, string imageName)
        : base(name, imageName)
    {
        this.itemNo = (int)ItemNo.PIXAXE;
    }
}