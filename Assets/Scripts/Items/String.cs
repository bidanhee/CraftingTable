
/// <summary>
/// �Ź���(��) ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class String : Item
{
    public String(string name) : base(name) { this.itemNo = (int)ItemNo.STRING; this.imageName = "String"; }
}
