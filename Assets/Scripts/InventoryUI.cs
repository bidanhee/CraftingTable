using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// 제작대 UI에 있는 제작 및 인벤토리를 총괄하는 클래스입니다.
/// </summary>
public class InventoryUI : MonoBehaviour
{

    /// <summary>
    /// 여기에 슬롯 오브젝트를 등록하면 됩니다.
    /// </summary>
    public GameObject iSlot;

    private const int INVENTORY_WIDTH = 9;
    private const int INVENTORY_HEIGHT = 4;
    private const int INVENTORY_SLOT_MAX = INVENTORY_WIDTH * INVENTORY_HEIGHT;
    private const int CRAFTING_WIDTH = 2;
    private const int CRAFTING_HEIGHT = 2;
    private const int CRAFTING_SLOT_MAX = CRAFTING_WIDTH * CRAFTING_HEIGHT;
    private const int EQUIPT_SLOT_MAX = 5;

    GameObject[] inventorySlot = new GameObject[INVENTORY_SLOT_MAX];        // 인벤토리 슬롯
    GameObject[] craftingSlot = new GameObject[CRAFTING_SLOT_MAX];     //제작대 슬롯
    GameObject[] equiptSlot = new GameObject[EQUIPT_SLOT_MAX];
    GameObject manufacturedSlot;    //완성품 슬롯

    public List<Item> items = new List<Item>();     //UI에서 관리하는 아이템리스트들. //추후에 Player의 item리스트와 연결해야합니다.
    void Awake()
    {
        makeSlots();
    }

    /// <summary>
    /// (빈)슬롯들을 만드는 함수 입니다.
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
    /// 아이템 리스트를 가져오는 함수입니다.
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
    /// 장착한 아이템 리스트를 가져오는 함수입니다.
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
    /// 슬롯에 아이템을 넣는 함수 입니다. 슬롯에 순서대로 넣어집니다.
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
    /// 장착된 아이템들을 슬롯에 넣는 함수입니다.
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
    /// 제작대에 올려진 아이템들을 확인하는 함수 입니다.
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
    /// 제작대에 올려진 아이템들을 전부 삭제하는 함수 입니다.
    /// 완성품을 다른 슬롯으로 옮길때 불러옵니다.
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
    /// 제작대에 올려진 아이템들로 제작품을 만들 수 있는가 확인하는 함수 입니다.
    /// 추후에 아이템 각각의 제작정보를 각각마다의 아이템 클래스로 옮기면 좋을 것 같습니다.
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
            CarrotFishingRod rod = new CarrotFishingRod("당근낚싯대");
            return rod;
        }
        return null;
    }
}
