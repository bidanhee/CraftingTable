using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// ���۴� UI�� �ִ� ���� �� �κ��丮�� �Ѱ��ϴ� Ŭ�����Դϴ�.
/// </summary>
public class CratftTableUI : MonoBehaviour
{

    /// <summary>
    /// ���⿡ ���� ������Ʈ�� ����ϸ� �˴ϴ�.
    /// </summary>
    public GameObject iSlot;

    private const int INVENTORY_WIDTH = 9;
    private const int INVENTORY_HEIGHT = 4;
    private const int INVENTORY_SLOT_MAX = INVENTORY_WIDTH * INVENTORY_HEIGHT;
    private const int CRAFTING_WIDTH = 3;
    private const int CRAFTING_HEIGHT = 3;
    private const int CRAFTING_SLOT_MAX = CRAFTING_WIDTH * CRAFTING_HEIGHT;

    GameObject[] inventorySlot = new GameObject[INVENTORY_SLOT_MAX];        // �κ��丮 ����
    GameObject[] craftingSlot = new GameObject[CRAFTING_SLOT_MAX];     //���۴� ����
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
        for (int i = 0; i < CRAFTING_HEIGHT; i++)
        {
            for (int j = 0; j < CRAFTING_WIDTH; j++)
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
        // ����׿�
        Debug.Log(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
            itemInCraft[0], itemInCraft[1], itemInCraft[2], itemInCraft[3],
            itemInCraft[4], itemInCraft[5], itemInCraft[6], itemInCraft[7], itemInCraft[8]));

        /////
        
        int[] craftPickaxe =
            {
            (int)ItemNo.IRON, (int)ItemNo.IRON, (int)ItemNo.IRON,
            (int)ItemNo.NONE, (int)ItemNo.STICK, (int)ItemNo.NONE,
            (int)ItemNo.NONE,(int)ItemNo.STICK, (int)ItemNo.NONE
            };

        int[] craftAxe =
        {
            (int)ItemNo.IRON, (int)ItemNo.IRON, (int)ItemNo.NONE,
            (int)ItemNo.IRON, (int)ItemNo.STICK, (int)ItemNo.NONE,
            (int)ItemNo.NONE,(int)ItemNo.STICK, (int)ItemNo.NONE
        };

        int[] craftSword =
        {
            (int)ItemNo.NONE, (int)ItemNo.IRON, (int)ItemNo.NONE,
            (int)ItemNo.NONE, (int)ItemNo.IRON, (int)ItemNo.NONE,
            (int)ItemNo.NONE,(int)ItemNo.STICK, (int)ItemNo.NONE
        };

        int[] craftShovel =
        {
            (int)ItemNo.NONE, (int)ItemNo.IRON, (int)ItemNo.NONE,
            (int)ItemNo.NONE, (int)ItemNo.STICK, (int)ItemNo.NONE,
            (int)ItemNo.NONE,(int)ItemNo.STICK, (int)ItemNo.NONE
        };

        int[] craftHoe =
        {
            (int)ItemNo.IRON, (int)ItemNo.IRON, (int)ItemNo.NONE,
            (int)ItemNo.NONE, (int)ItemNo.STICK, (int)ItemNo.NONE,
            (int)ItemNo.NONE,(int)ItemNo.STICK, (int)ItemNo.NONE
        };

        int[] craftBow =
        {
            (int)ItemNo.NONE, (int)ItemNo.STICK, (int)ItemNo.STRING,
            (int)ItemNo.STICK, (int)ItemNo.NONE, (int)ItemNo.STRING,
            (int)ItemNo.NONE,(int)ItemNo.STICK, (int)ItemNo.STRING
        };

        int[] craftFishingRod =
        {
            (int)ItemNo.NONE, (int)ItemNo.NONE, (int)ItemNo.STICK,
            (int)ItemNo.NONE, (int)ItemNo.STICK, (int)ItemNo.STRING,
            (int)ItemNo.STICK,(int)ItemNo.NONE, (int)ItemNo.STRING
        };

        int[] craftCarrotFishingRod =
        {
            (int)ItemNo.FISHINGROD, (int)ItemNo.CARROT
        };
        
        //////

        if (itemInCraft.SequenceEqual(craftPickaxe))
        {
            Pickaxe pickaxe = new Pickaxe("���");
            return pickaxe;
        }
        else if(itemInCraft.SequenceEqual(craftAxe))
        {
            Axe axe = new Axe("����");
            return axe;
        }
        else if (itemInCraft.SequenceEqual(craftSword))
        {
            Sword sword = new Sword("��");
            return sword;
        }
        else if (itemInCraft.SequenceEqual(craftShovel))
        {
            Shovel shovel = new Shovel("��");
            return shovel;
        }
        else if (itemInCraft.SequenceEqual(craftBow))
        {
            Bow bow = new Bow("Ȱ");
            return bow;
        }
        else if (itemInCraft.SequenceEqual(craftHoe))
        {
            Hoe hoe = new Hoe("����");
            return hoe;
        }
        else if (itemInCraft.SequenceEqual(craftFishingRod))
        {
            FishingRod rod = new FishingRod("���˴�");
            return rod;
        }
        else if (craftCarrotFishingRod.All(item => itemInCraft.Contains(item)))
        {
            CarrotFishingRod rod = new CarrotFishingRod("��ٳ��˴�");
            return rod;
        }
        return null;
    }
}
