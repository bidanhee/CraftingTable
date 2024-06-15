using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// ���۴� UI�� �ִ� ���� �� �κ��丮�� �Ѱ��ϴ� Ŭ�����Դϴ�.
/// </summary>
public class InventoryUI : MonoBehaviour
{

    /// <summary>
    /// ���⿡ ���� ������Ʈ�� ����ϸ� �˴ϴ�.
    /// </summary>
    public GameObject iSlot;

    private const int INVENTORY_WIDTH = 9;
    private const int INVENTORY_HEIGHT = 4;
    private const int INVENTORY_SLOT_MAX = INVENTORY_WIDTH * INVENTORY_HEIGHT;
    private const int CRAFTING_WIDTH = 2;
    private const int CRAFTING_HEIGHT = 2;
    private const int CRAFTING_SLOT_MAX = CRAFTING_WIDTH * CRAFTING_HEIGHT;
    private const int EQUIPT_SLOT_MAX = 5;

    GameObject[] inventorySlot = new GameObject[INVENTORY_SLOT_MAX];        // �κ��丮 ����
    GameObject[] craftingSlot = new GameObject[CRAFTING_SLOT_MAX];     //���۴� ����
    GameObject[] equiptSlot = new GameObject[EQUIPT_SLOT_MAX];
    GameObject manufacturedSlot;    //�ϼ�ǰ ����

    public List<Item> items = new List<Item>();     //UI���� �����ϴ� �����۸���Ʈ��. //���Ŀ� Player�� item����Ʈ�� �����ؾ��մϴ�.
    void Awake()
    {
        makeSlots();
    }

    /// <summary>
    /// (��)���Ե��� ����� �Լ� �Դϴ�.
    /// </summary>
    public void makeSlots()
    {
        Slot slot;
        //inventorySlot
        int cnt = 0;
        for (int i = 0; i < INVENTORY_HEIGHT; i++)
        {
            for (int j = 0; j < INVENTORY_WIDTH; j++)
            {
                inventorySlot[cnt] = Instantiate(iSlot, transform);
                inventorySlot[cnt].SetActive(true);
                slot = inventorySlot[cnt].GetComponent<Slot>();
                if (i == INVENTORY_HEIGHT - 1)
                {
                    inventorySlot[cnt].transform.position = new Vector2(568 + (j * 98), 500 - (i * 100 + 18));
                    slot.type = (int)SlotType.QUICKSLOT;
                }
                else
                {
                    inventorySlot[cnt].transform.position = new Vector2(568 + (j * 98), 500 - (i * 100));
                    slot.type = (int)SlotType.INVENTORY;
                }
                cnt++;
            }
        }

        //craftingSlot
        cnt = 0;
        for (int i = 0; i < CRAFTING_HEIGHT; i++)
        {
            for (int j = 0; j < CRAFTING_WIDTH; j++)
            {
                craftingSlot[cnt] = Instantiate(iSlot, transform);
                craftingSlot[cnt].SetActive(true);
                craftingSlot[cnt].transform.position = new Vector2(1050 + (j * 98), 850 - (i * 100));
                slot = craftingSlot[cnt].GetComponent<Slot>();
                slot.type = (int)SlotType.CRAFT;
                cnt++;
                slot.craftNum = cnt;
            }
        }

        //manufacturedSlot
        manufacturedSlot = Instantiate(iSlot, transform);
        manufacturedSlot.SetActive(true);
        manufacturedSlot.transform.position = new Vector2(1360, 790);
        slot = manufacturedSlot.GetComponent<Slot>();
        slot.type = (int)SlotType.MANUFACTURED;

        cnt = 0;
        for (int i = 0; i < EQUIPT_SLOT_MAX; i++)
        {
            equiptSlot[cnt] = Instantiate(iSlot, transform);
            equiptSlot[cnt].SetActive(true);
            if (i == (EQUIPT_SLOT_MAX - 1))
            {
                equiptSlot[cnt].transform.position = new Vector2(955, 915 - ((i-1) * 100));
            }
            else
            {
                equiptSlot[cnt].transform.position = new Vector2(570, 915 - (i * 100));
            }
            slot = equiptSlot[cnt].GetComponent<Slot>();
            slot.type = (int)SlotType.EQUIPT;
            cnt++;
            slot.craftNum = cnt;
        }

    }

    /// <summary>
    /// ������ ����Ʈ�� �������� �Լ��Դϴ�.
    /// </summary>
    public List<Item> getItemList()
    {
        List<Item> itemInUI = new List<Item>();
        Slot slot = null;
        for (int i = 0; i < CRAFTING_SLOT_MAX; i++)
        {
            slot = craftingSlot[i].GetComponent<Slot>();
            if (slot.isHaveItem)
            {
                itemInUI.Add(slot.item);
            }
        }

        for (int i = 0; i < INVENTORY_SLOT_MAX; i++)
        {
            slot = inventorySlot[i].GetComponent<Slot>();
            if (slot.isHaveItem)
            {
                itemInUI.Add(slot.item);
            }
        }
        slot = manufacturedSlot.GetComponent<Slot>();
        if (slot.isHaveItem)
        {
            itemInUI.Add(slot.item);
        }

        return itemInUI;
    }

    /// <summary>
    /// ������ ������ ����Ʈ�� �������� �Լ��Դϴ�.
    /// </summary>
    public List<Item> getEquiptedItemList() 
    {
        List<Item> itemInUI = new List<Item>();
        Slot slot = null;
        for (int i = 0; i < EQUIPT_SLOT_MAX; i++)
        {
            slot = null;
            slot = equiptSlot[i].GetComponent<Slot>();
            if(slot.isHaveItem) itemInUI.Add(slot.item);
            else itemInUI.Add(null);
        }
        return itemInUI;
    }
    /// <summary>
    /// ���Կ� �������� �ִ� �Լ� �Դϴ�. ���Կ� ������� �־����ϴ�.
    /// </summary>
    public void AddItemToSlot(Item item)
    {
        int curIndex = 0;
        while (true)
        {
            if (curIndex >= INVENTORY_SLOT_MAX) { break; }

            Slot itemSlot = inventorySlot[curIndex].GetComponent<Slot>();
            if(itemSlot.isHaveItem)
            {
                curIndex++;
            }
            else
            {
                itemSlot.item = item;
                itemSlot.isHaveItem = true;
                itemSlot.UpdateItemImage();
                break;
            }
        }
    }
    /// <summary>
    /// ������ �����۵��� ���Կ� �ִ� �Լ��Դϴ�.
    /// </summary>
    public void AddEquiptedToSlot(List<Item> eItems)
    {

        for (int i = 0; i < EQUIPT_SLOT_MAX; i++)
        {
            Slot itemSlot = equiptSlot[i].GetComponent<Slot>();
            if (eItems[i] != null)
            {
                itemSlot.item = eItems[i];
                itemSlot.isHaveItem = true;
                itemSlot.UpdateItemImage();
            }
        }
    }


    /// <summary>
    /// ���۴뿡 �÷��� �����۵��� Ȯ���ϴ� �Լ� �Դϴ�.
    /// </summary>
    public void CheckCraftTable()
    {
        int[] itemInCraft = new int[CRAFTING_SLOT_MAX];
        Slot[] _craftSlot = new Slot[CRAFTING_SLOT_MAX];
        for (int i = 0; i < CRAFTING_SLOT_MAX; i++)
        {
            _craftSlot[i] = craftingSlot[i].GetComponent<Slot>();
            itemInCraft[i] = _craftSlot[i].GetItemNo();
        }

        Slot _manufacturedSlot = manufacturedSlot.GetComponent<Slot>();
        _manufacturedSlot.RemoveItem(_manufacturedSlot);
        if (CheckCraftingItems(itemInCraft) != null)
        {
            _manufacturedSlot.AddItem(CheckCraftingItems(itemInCraft));
        }
    }

    /// <summary>
    /// ���۴뿡 �÷��� �����۵��� ���� �����ϴ� �Լ� �Դϴ�.
    /// �ϼ�ǰ�� �ٸ� �������� �ű涧 �ҷ��ɴϴ�.
    /// </summary>
    public void RemoveCraftingSlots()
    {
        Slot[] _craftSlot = new Slot[CRAFTING_SLOT_MAX];
        for (int i = 0; i < CRAFTING_SLOT_MAX; i++)
        {
            _craftSlot[i] = craftingSlot[i].GetComponent<Slot>();
            _craftSlot[i].RemoveItem(_craftSlot[i]);
        }
    }

    /// <summary>
    /// ���۴뿡 �÷��� �����۵�� ����ǰ�� ���� �� �ִ°� Ȯ���ϴ� �Լ� �Դϴ�.
    /// ���Ŀ� ������ ������ ���������� ���������� ������ Ŭ������ �ű�� ���� �� �����ϴ�.
    /// </summary>
    public Item CheckCraftingItems(int[] itemInCraft)
    {

        int[] craftCarrotFishingRod =
        {
            (int)ItemNo.FISHINGROD, (int)ItemNo.CARROT
        };
        
        //////

        if (craftCarrotFishingRod.All(item => itemInCraft.Contains(item)))
        {
            CarrotFishingRod rod = new CarrotFishingRod("��ٳ��˴�");
            return rod;
        }
        return null;
    }
}
