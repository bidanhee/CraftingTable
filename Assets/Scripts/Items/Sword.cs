/// <summary>
/// �� ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Sword : Item
{
    public Sword(string name)
        : base(name)
    {
        this.itemNo = (int)ItemNo.SWORD;
        this.imageName = "Sword";
    }
}