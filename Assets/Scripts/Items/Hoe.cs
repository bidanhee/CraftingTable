/// <summary>
/// 괭이 아이템 클래스 입니다.
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