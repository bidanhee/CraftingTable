/// <summary>
/// ��� ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Carrot : Item
{
    public Carrot(string name) : base(name)
    {
        this.itemNo = (int)ItemNo.CARROT;
        this.imageName = "Carrot";
    }
}