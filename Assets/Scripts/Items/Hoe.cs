/// <summary>
/// ���� ������ Ŭ���� �Դϴ�.
/// </summary>
[System.Serializable]
public class Hoe : Item
{
    public Hoe(string name) : base(name) 
    { 
        this.itemNo = (int)ItemNo.HOE; 
        this.imageName = "Hoe"; 
    }
}