/// <summary>
/// ö ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Iron : Item
{
    public Iron(string name) : base(name) 
    { this.itemNo = (int)ItemNo.IRON;
        this.imageName = "Iron"; }
}