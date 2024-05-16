using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ���Ը��� �ο��Ǵ� ������ Ÿ�Ե��Դϴ�.
/// </summary>
enum SlotType { ERROR, QUICKSLOT, INVENTORY, CRAFT, MANUFACTURED};

/// <summary>
/// ���� Ŭ���� �Դϴ�.
/// </summary>
public class Slot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    CratftTableUI table;    
    public Item item = null;    //������ �����ϰ� �ִ� �������Դϴ�.
    public Image image;     //�������� ������ �� �̹���, �������� ������ �������̹����� ���ϴ�.

    public bool isHaveItem; //������ �������� �����ϰ� �ִ°�?
    Vector3 startPosition;  //�巡�� �� �̿��ϴ� �����Դϴ�.
    public int type;    // ���Ը��� �ο��Ǵ� ������ Ÿ���Դϴ�.
    public int craftNum;    // ���۴� ���Կ��� �ο��Ǵ� ��ȣ�Դϴ�. ���۴� ������ �ƴ� �� 0�Դϴ�.
    public bool isDrag; //�巡�� ���ΰ�?


    void Update()
    {
        RButtonDownOnDrag();
    }

    /// <summary>
    /// �巡�� �� ��Ŭ���� �ҷ����� �Լ��Դϴ�.
    /// �������� �ϳ��� ��� �մϴ�.
    /// </summary>
    void RButtonDownOnDrag()
    {
        if (Input.GetMouseButtonDown(1) && isDrag)
        {
            // ���콺 ��ġ�� �ִ� UI ��Ҹ� �����ɴϴ�.
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // ���콺 ��ġ�� �ٸ� ������ �ִ��� Ȯ���մϴ�.
            foreach (RaycastResult result in results)
            {
                Slot otherSlot = result.gameObject.GetComponent<Slot>();
                if (otherSlot != null && otherSlot != this)
                {
                    // �ٸ� ���Կ� �������� ����մϴ�.
                    AddItem(otherSlot, this.item);
                }
            }
        }
    }

    /// <summary>
    /// �巡�� ���۶� �ҷ����� �Լ��Դϴ�.
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isHaveItem)
        {
            startPosition = transform.position;
            isDrag = true;
        }
    }

    /// <summary>
    /// �巡�� �߿� �ҷ����� �Լ��Դϴ�.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (isHaveItem)
        {
            transform.position = eventData.position;
            transform.SetAsLastSibling();
        }
    }

    /// <summary>
    /// �巡�� ���� �� �ҷ����� �Լ��Դϴ�.
    /// �巡�װ� ����� ��ġ�� ���԰� �������� ��ȯ�մϴ�.
    /// </summary>
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
                    // �ٸ� ���԰� �������� ��ȯ�մϴ�.
                    SwapItems(otherSlot);
                }
            }

            transform.position = startPosition;

            // �� ������ �ϼ�ǰ�ϰ��, ������ ��ȯ �� ���۴��� �������� �����մϴ�.
            if(!isHaveItem && this.type == (int)SlotType.MANUFACTURED)
            {
                table.RemoveCraftingSlots();
            }
        }
        isDrag = false;
    }

    /// <summary>
    /// Ư�� ���Կ� �������� �߰��ϴ� �Լ��Դϴ�.
    /// </summary>
    public void AddItem(Slot otherSlot, Item item)
    {
        if (!otherSlot.isHaveItem && otherSlot.type != (int)SlotType.MANUFACTURED)
        {
            otherSlot.item = item;
            otherSlot.isHaveItem = true;
            otherSlot.UpdateItemImage();
        }
    }

    /// <summary>
    /// �� ���Կ� �������� �߰��ϴ� �Լ��Դϴ�.
    /// </summary>
    public void AddItem(Item item)
    {
        if (!isHaveItem)
        {
            this.item = item;
            this.isHaveItem = true;
            UpdateItemImage();
        }
    }

    /// <summary>
    /// �� ������ �������� �����ϴ� �Լ��Դϴ�.
    /// </summary>
    public void RemoveItem(Slot slot)
    {
        if (isHaveItem)
        {
            slot.item = null;
            this.isHaveItem = false;
            UpdateItemImage();
        }
    }

    /// <summary>
    /// �ٸ� ���԰� �������� ��ȯ�ϴ� �Լ��Դϴ�.
    /// </summary>
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

    /// <summary>
    /// ������ �̹����� ������Ʈ�ϴ� �Լ��Դϴ�.
    /// </summary>
    public void UpdateItemImage()
    {
        //�������� ���� ���� �� �������̹����� �������ϴ�.
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
        //�������� ������ �� �������̹����� �������ϴ�.
        //����� Magenta�� ����մϴ�.
        else
        {
            image.sprite = null;
            Color tempColor = image.color;
            tempColor.a = 0.5f;
            tempColor.r = 1f;
            tempColor.g = 0f;
            tempColor.b = 1f;
            image.color = tempColor;
        }

        // ���۴� ������ ������Ʈ �� �� ����, ���۴븦 Ȯ���մϴ�.
        if (this.type == (int)SlotType.CRAFT)
        {
            table.CheckCraftTable();
        }
    }

    /// <summary>
    /// ���Կ� �ִ� �������� ��ȣ�� �������� �Լ��Դϴ�.
    /// </summary>
    public int GetItemNo()
    {
        int num = 0;
        if (isHaveItem)
        {
            num = this.item.itemNo;
        }
        return num;
    }

    /// <summary>
    /// ������ ��Ŭ�� �� �ҷ����� �Լ��Դϴ�.
    /// �巡������ �ƴ� �� ��Ŭ���� ���Կ� �ִ� �������� �����մϴ�.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !isDrag)
        {
            RemoveItem(this);
        }
    }
}
