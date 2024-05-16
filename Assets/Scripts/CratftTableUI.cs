using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class CratftTableUI : MonoBehaviour
{    
    public GameObject iSlot;

    GameObject[] inventorySlot = new GameObject[36];
    GameObject[] craftingSlot = new GameObject[9];
    GameObject manufacturedSlot;

    public List<Item> items = new List<Item>();
    void Awake()
    {
        makeSlots();
    }

    public void makeSlots()
    {
        Slot slot;
        //inventorySlot
        int cnt = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                inventorySlot[cnt] = Instantiate(iSlot, transform);
                inventorySlot[cnt].SetActive(true);
                slot = inventorySlot[cnt].GetComponent<Slot>();
                if (i == 3)
                {
                    inventorySlot[cnt].transform.position = new Vector2(518 + (j * 110), 485 - (i * 120 + 20));
                    slot.type = (int)SlotType.QUICKSLOT;
                }
                else
                {
                    inventorySlot[cnt].transform.position = new Vector2(518 + (j * 110), 485 - (i * 120));
                    slot.type = (int)SlotType.INVENTORY;
                }
                cnt++;
            }
        }

        //craftingSlot
        cnt = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                craftingSlot[cnt] = Instantiate(iSlot, transform);
                craftingSlot[cnt].SetActive(true);
                craftingSlot[cnt].transform.position = new Vector2(655 + (j * 110), 920 - (i * 120));
                slot = craftingSlot[cnt].GetComponent<Slot>();
                slot.type = (int)SlotType.CRAFT;
                cnt++;
                slot.craftNum = cnt;
            }
        }

        //manufacturedSlot
        manufacturedSlot = Instantiate(iSlot, transform);
        manufacturedSlot.SetActive(true);
        manufacturedSlot.transform.position = new Vector2(1230, 800);
        slot = manufacturedSlot.GetComponent<Slot>();
        slot.type = (int)SlotType.MANUFACTURED;

    }

    public void AddItemToSlot(Item item)
    {
        int curIndex = 0;
        while (true)
        {
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

    public void CheckCraftTable()
    {
        int[] itemInCraft = new int[9];
        Slot[] _craftSlot = new Slot[9];
        for (int i = 0; i < 9; i++)
        {
            _craftSlot[i] = craftingSlot[i].GetComponent<Slot>();
            itemInCraft[i] = _craftSlot[i].GetItemNo();
        }

        Slot _manufacturedSlot = manufacturedSlot.GetComponent<Slot>();
        if (CheckCraftingItems(itemInCraft) != null)
        {
            _manufacturedSlot.AddItem(CheckCraftingItems(itemInCraft));
        }
    }
    public void RemoveCraftingSlots()
    {
        Slot[] _craftSlot = new Slot[9];
        for (int i = 0; i < 9; i++)
        {
            _craftSlot[i] = craftingSlot[i].GetComponent<Slot>();
            _craftSlot[i].RemoveItem(_craftSlot[i]);
        }
    }

    public Item CheckCraftingItems(int[] itemInCraft)
    {
        Debug.Log(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
            itemInCraft[0], itemInCraft[1], itemInCraft[2], itemInCraft[3],
            itemInCraft[4], itemInCraft[5], itemInCraft[6], itemInCraft[7], itemInCraft[8]));

        int[] craftPickaxe =
            {
            (int)ItemNo.IRON, (int)ItemNo.IRON, (int)ItemNo.IRON,
            (int)ItemNo.NONE, (int)ItemNo.STICK, (int)ItemNo.NONE,
            (int)ItemNo.NONE,(int)ItemNo.STICK, (int)ItemNo.NONE
            };

        if (itemInCraft.SequenceEqual(craftPickaxe))
        {
            Pickaxe pickaxe = new Pickaxe("°î±ªÀÌ", "Pickaxe");
            return pickaxe;
        }

        return null;
    }
}
