/// <summary>
/// Ȱ ������ Ŭ���� �Դϴ�.
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