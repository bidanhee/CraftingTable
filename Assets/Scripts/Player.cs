using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �÷��̾� Ŭ���� �Դϴ�.
/// ������ �����۵��� �߰� �� �����ϱ� ���� ���Ǿ����ϴ�.
/// </summary>
public class Player : MonoBehaviour
{
    public GameObject CraftUI; 
    private GameObject _CraftUI;
    public List<Item> items = new List<Item>();  //�����۵��� ����Ǵ� ����Ʈ�Դϴ�.

    public Button addBasicItemButton;   //�⺻�������� �߰��ϴ� ��ư�Դϴ�.

    private void Start()
    {
        AddBasicItem();
        _CraftUI = Instantiate(CraftUI, transform);
        _CraftUI.SetActive(true);
        DisplayItemToUI();
        addBasicItemButton = GetComponentInChildren<Button>();
        addBasicItemButton.onClick.AddListener(DisplayItemToUI);
    }

    /// <summary>
    /// �⺻�������� �߰��ϴ� �׽�Ʈ�� �Լ� �Դϴ�.
    /// </summary>
    public void AddBasicItem()
    {
        Stick stick= new Stick("�����");
        String _string = new String("��");
        Iron iron = new Iron("ö");
        Carrot carrot = new Carrot("���");

        items.Add(stick);
        items.Add(_string);
        items.Add(iron);
        items.Add(carrot);
    }

    /// <summary>
    /// �����ִ� ������ ����Ʈ���� ui�� ���Կ� �߰��ϴ� �Լ��Դϴ�.
    /// </summary>
    public void DisplayItemToUI()
    {
        foreach (var item in items)
        {
            CratftTableUI ui = _CraftUI.GetComponent<CratftTableUI>();
            ui.AddItemToSlot(item);
        }
    }

    /// <summary>
    /// ������ ����Ʈ�� �������� �Լ��Դϴ�.
    /// </summary>
    public List<Item> GetItemList()
    {
        return items;
    }

    /// <summary> TODO) �������� ��ų� �����Ҷ����� ����Ʈ�� �����ؾ��մϴ�.
    /// ����� ����Ʈ�� �߰��ؼ� �� ����� �����Ƿ� ����Ӵϴ�.
    /// </summary>
    public void AddItemToList(Item item)
    {

    }

}