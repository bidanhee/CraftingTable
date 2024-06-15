using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 클래스 입니다.
/// 보유한 아이템들을 추가 및 저장하기 위해 사용되었습니다.
/// </summary>
public class Player : MonoBehaviour
{
    public GameObject CraftUI;
    public GameObject InventoryUI;
    private GameObject _CraftUI;
    private GameObject _InventoryUI;
    public List<Item> items = new List<Item>();  //아이템들이 저장되는 리스트입니다.
    public List<Item> equiptedItems = new List<Item>();  //아이템들이 저장되는 리스트입니다.

    public Button addBasicItemButton;   //기본아이템을 추가하는 버튼입니다.

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
    /// 기본아이템을 추가하는 테스트용 함수 입니다.
    /// </summary>
    public void AddBasicItem()
    {
        Stick stick= new Stick("막대기");
        String _string = new String("실");
        Iron iron = new Iron("철");
        Carrot carrot = new Carrot("당근");

        items.Add(stick);
        items.Add(_string);
        items.Add(iron);
        items.Add(carrot);
    }

    /// <summary>
    /// 갖고있는 아이템 리스트들을 ui의 슬롯에 추가하는 함수입니다.
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
    /// 갖고있는 아이템 리스트들을 ui의 슬롯에 추가하는 함수입니다.
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
    /// 아이템 리스트를 가져오는 함수입니다.
    /// </summary>
    public List<Item> GetItemList()
    {
        return items;
    }

    /// <summary> 
    /// 아이템을 리스트에 넣는 함수입니다.
    /// </summary>
    public void AddItemToList(Item item)
    {
        Item newItem = item;
        items.Add(item);
    }

    /// <summary> 
    /// 장착한 아이템들의 이미지를 업데이트하는 함수입니다.
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
                case 3: // Boots (양쪽)
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