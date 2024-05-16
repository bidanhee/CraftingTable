using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum SlotType { ERROR, QUICKSLOT, INVENTORY, CRAFT, MANUFACTURED};
public class Slot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public Item item = null;
    public bool isHaveItem;
    Vector3 startPosition;
    public int type;
    public int craftNum;
    public bool isDrag;
    public CratftTableUI table;

    void Update()
    {
        RButtonDownOnDrag();
    }
    void RButtonDownOnDrag()
    {
        if (Input.GetMouseButtonDown(1) && isDrag)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                Slot otherSlot = result.gameObject.GetComponent<Slot>();
                if (otherSlot != null && otherSlot != this)
                {
                    AddItem(otherSlot, this.item);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isHaveItem)
        {
            startPosition = transform.position;
            isDrag = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isHaveItem)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isHaveItem)
        {
            // ���콺 ��ġ�� �ִ� UI ��Ҹ� �����ɴϴ�.
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // ���콺 ��ġ�� �ٸ� ������ �ִ��� Ȯ���մϴ�.
            foreach (RaycastResult result in results)
            {
                Slot otherSlot = result.gameObject.GetComponent<Slot>();
                if (otherSlot != null && otherSlot != this)
                {
                    // �ٸ� ���Կ� �������� ����մϴ�.
                    SwapItems(otherSlot);
                }
            }

            transform.position = startPosition;

            if(!isHaveItem && this.type == (int)SlotType.MANUFACTURED)
            {
                table.RemoveCraftingSlots();
            }
        }
        isDrag = false;
    }

    public void AddItem(Slot otherSlot, Item item)
    {
        /*if (this.type == (int)SlotType.MANUFACTURED && !otherSlot.isHaveItem)
        {
            otherSlot.item = item;
            otherSlot.isHaveItem = true;
            otherSlot.UpdateItemImage();
            RemoveItem(this);
        }*/

        if (!otherSlot.isHaveItem && otherSlot.type != (int)SlotType.MANUFACTURED/* && this.type != (int)SlotType.MANUFACTURED*/)
        {
            otherSlot.item = item;
            otherSlot.isHaveItem = true;
            otherSlot.UpdateItemImage();
        }
    }

    public void AddItem(Item item)
    {
        if (!isHaveItem)
        {
            this.item = item;
            this.isHaveItem = true;
            UpdateItemImage();
        }
    }

    public void RemoveItem(Slot slot)
    {
        if (isHaveItem)
        {
            slot.item = null;
            this.isHaveItem = false;
            UpdateItemImage();
        }
    }


    void SwapItems(Slot otherSlot)
    {
        if (otherSlot.type != (int)SlotType.MANUFACTURED)
        {
            // �� ���԰� �ٸ� ������ �������� ��ȯ�մϴ�.
            if (otherSlot.isHaveItem && this.type != (int)SlotType.MANUFACTURED)
            {
                Item tempItem = otherSlot.item;
                otherSlot.item = this.item;
                this.item = tempItem;
            }
            else if (!otherSlot.isHaveItem)
            {
                otherSlot.isHaveItem = true;
                this.isHaveItem = false;
                otherSlot.item = this.item;
            }

            // UI�� ������Ʈ�մϴ�.
            UpdateItemImage();
            otherSlot.UpdateItemImage();
        }
    }

    void Awake()
    {
        image = GetComponent<Image>();
        table = GetComponentInParent<CratftTableUI>();
        isHaveItem = false;
    }

    public void UpdateItemImage()
    {
        if (isHaveItem)
        {
            Sprite newImage = Resources.Load<Sprite>(item.imageName);
            if (newImage != null)
            {
                image.sprite = newImage;
                Color tempColor = image.color; 
                tempColor.a = 1f;
                tempColor.g = 1f;
                image.color = tempColor; 
            }
            else
            {
                Debug.LogError("�̹����� ã�� �� �����ϴ�: " + item.name);
            }
        }
        else
        {
            image.sprite = null;
            Color tempColor = image.color;
            tempColor.a = 0.5f;
            tempColor.g = 0f;
            image.color = tempColor;
        }

        if (this.type == (int)SlotType.CRAFT)
        {
            table.CheckCraftTable();
        }
    }

    public int GetItemNo()
    {
        int num = 0;
        if (isHaveItem)
        {
            num = this.item.itemNo;
        }
        return num;
    }

}
