
/// <summary>
/// �Ź���(��) ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class String : Item
{
    public String(string name, string imageName)
        : base(name, imageName)
    {
        this.itemNo = (int)ItemNo.STRING;
    }
}
