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
    public GameObject InventoryUI;
    private GameObject _CraftUI;
    private GameObject _InventoryUI;
    public List<Item> items = new List<Item>();  //�����۵��� ����Ǵ� ����Ʈ�Դϴ�.
    public List<Item> equiptedItems = new List<Item>();  //�����۵��� ����Ǵ� ����Ʈ�Դϴ�.

    public Button addBasicItemButton;   //�⺻�������� �߰��ϴ� ��ư�Դϴ�.

    public Button craftingButton;
    public Button inventoryButton;
    public Button exitButton;

    public GameObject iHead;
    public GameObject iBody;
    public GameObject iPants;
    public GameObject iBoots1;
    public GameObject iBoots2;
    public GameObject iHand;

    private void Start()
    {
        AddBasicItem();
        craftingButton.onClick.AddListener(OpenCraftingTable);
        inventoryButton.onClick.AddListener(OpenInventory);
        exitButton.onClick.AddListener(ExitUI);
        exitButton.interactable = false;
        for (int i = 0; i < 5; i++) equiptedItems.Add(null);
        UpdateEquiptmentImage();
    }

    public void OpenCraftingTable()
    {
        _CraftUI = Instantiate(CraftUI, transform);
        _CraftUI.SetActive(true);
        DisplayItemToCraftingUI();
        addBasicItemButton = GetComponentInChildren<Button>();
        addBasicItemButton.onClick.AddListener(DisplayItemToCraftingUI);
        craftingButton.interactable = false; inventoryButton.interactable = false;
        exitButton.interactable = true;
        Time.timeScale = 0.0f;
    }

    public void OpenInventory()
    {
        _InventoryUI = Instantiate(InventoryUI, transform);
        _InventoryUI.SetActive(true);
        DisplayItemToInventoryUI();
         _InventoryUI.GetComponent<InventoryUI>().AddEquiptedToSlot(equiptedItems);
        craftingButton.interactable = false; inventoryButton.interactable = false;
        exitButton.interactable = true;
        Time.timeScale = 0.0f;
    }

    public void ExitUI()
    {
        if (_CraftUI)
        {
            CratftTableUI ui = _CraftUI.GetComponent<CratftTableUI>();
            items.Clear();
            items = ui.getItemList();
            Destroy(_CraftUI);
        }
        if (_InventoryUI)
        {
            InventoryUI ui = _InventoryUI.GetComponent<InventoryUI>();
            items.Clear(); equiptedItems.Clear();
            items = ui.getItemList();
            equiptedItems = ui.getEquiptedItemList();
            UpdateEquiptmentImage();
            Destroy(_InventoryUI);
        }
        Time.timeScale = 1.0f;
        craftingButton.interactable = true; inventoryButton.interactable = true;
        exitButton.interactable = false;
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
    public void DisplayItemToCraftingUI()
    {
        foreach (var item in items)
        {
            if (_CraftUI)
            {
                CratftTableUI ui = _CraftUI.GetComponent<CratftTableUI>();
                ui.AddItemToSlot(item);
            }
        }
    }

    /// <summary>
    /// �����ִ� ������ ����Ʈ���� ui�� ���Կ� �߰��ϴ� �Լ��Դϴ�.
    /// </summary>
    public void DisplayItemToInventoryUI()
    {
        foreach (var item in items)
        {
            if (_InventoryUI)
            {
                InventoryUI ui = _InventoryUI.GetComponent<InventoryUI>();
                ui.AddItemToSlot(item);
            }
        }
    }

    /// <summary>
    /// ������ ����Ʈ�� �������� �Լ��Դϴ�.
    /// </summary>
    public List<Item> GetItemList()
    {
        return items;
    }

    /// <summary> 
    /// �������� ����Ʈ�� �ִ� �Լ��Դϴ�.
    /// </summary>
    public void AddItemToList(Item item)
    {
        Item newItem = item;
        items.Add(item);
    }

    /// <summary> 
    /// ������ �����۵��� �̹����� ������Ʈ�ϴ� �Լ��Դϴ�.
    /// </summary>
    public void UpdateEquiptmentImage()
    {
        for (int i = 0; i < 5; i++)
        {
            Sprite newImage = null;
            if (equiptedItems[i] != null)
            {
                newImage = Resources.Load<Sprite>(equiptedItems[i].imageName);
            }
            switch (i)
            {
                case 0: // Head
                    iHead.GetComponent<SpriteRenderer>().sprite = newImage;
                    break;
                case 1: // Body
                    iBody.GetComponent<SpriteRenderer>().sprite = newImage;
                    break;
                case 2: // Pants
                    iPants.GetComponent<SpriteRenderer>().sprite = newImage;
                    break;
                case 3: // Boots (����)
                    iBoots1.GetComponent<SpriteRenderer>().sprite = newImage;
                    iBoots2.GetComponent<SpriteRenderer>().sprite = newImage;
                    break;
                case 4: // Hand
                    iHand.GetComponent<SpriteRenderer>().sprite = newImage;
                    break;
            }
        }
    }
}