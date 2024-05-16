using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// �÷��̾� Ŭ���� �Դϴ�.
/// ������ �����۵��� �߰� �� �����ϱ� ���� ���˴ϴ�.
/// </summary>
public class Player : MonoBehaviour
{
    public GameObject CraftUI;
    private GameObject _CraftUI;
    public List<Item> items = new List<Item>();  //�����۵��� ����Ǵ� ����Ʈ�Դϴ�.

    private void Start()
    {
        AddBasicItem();
        _CraftUI = Instantiate(CraftUI, transform);
        _CraftUI.SetActive(true);
        DisplayItemToUI();
    }
    
    public void AddBasicItem()
    {
        Stick stick= new Stick("�����", "Stick");
        String _string = new String("��", "String");
        Iron iron = new Iron("ö", "Iron");

        items.Add(stick);
        items.Add(_string);
        items.Add(iron);
    }

    public void DisplayItemToUI()
    {
        foreach (var item in items)
        {
            CratftTableUI ui = _CraftUI.GetComponent<CratftTableUI>();
            ui.AddItemToSlot(item);
        }
    }

    public List<Item> GetItemList()
    {
        return items;
    }

    //TODO) �������� ��ų� �����Ҷ����� ����Ʈ�� �����ؾ��մϴ�.
    //����� ����Ʈ�� �߰��ؼ� �� ����� �����Ƿ� ����Ӵϴ�.
    public void AddItemToList(Item item)
    {

    }

}