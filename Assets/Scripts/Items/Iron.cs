/// <summary>
/// ö ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Iron : Item
{
    public Iron(string name, string imageName)
        : base(name, imageName)
    {
        this.itemNo = (int)ItemNo.IRON;
    }
}