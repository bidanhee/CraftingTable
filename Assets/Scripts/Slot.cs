using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 슬롯마다 부여되는 슬롯의 타입들입니다.
/// </summary>
enum SlotType { ERROR, QUICKSLOT, INVENTORY, CRAFT, MANUFACTURED, EQUIPT};

/// <summary>
/// 슬롯 클래스 입니다.
/// </summary>
public class Slot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    CratftTableUI cTable = null;    // 제작대
    InventoryUI iTable = null;    // 인벤토리

    public Item item = null;    //슬롯이 보유하고 있는 아이템입니다.
    public Image image;     //아이템이 없으면 빈 이미지, 아이템이 있으면 아이템이미지가 들어갑니다.

    public bool isHaveItem; //슬롯이 아이템을 보유하고 있는가?
    Vector3 startPosition;  //드래그 시 이용하는 벡터입니다.
    public int type;    // 슬롯마다 부여되는 슬롯의 타입입니다.
    public int craftNum;    // 제작대 슬롯에만 부여되는 번호입니다. 제작대 슬롯이 아닐 시 0입니다.
    public bool isDrag; //드래그 중인가?

    public GameObject tooltip;  //툴팁
    public TextMeshProUGUI tooltipText; //툴팁텍스트

    void Update()
    {
        RButtonDownOnDrag();
        if(isDrag && tooltip.activeSelf)
        {
            tooltip.SetActive(false);   //툴팁 OFF
        }
    }

    /// <summary>
    /// 드래그 중 우클릭때 불러지는 함수입니다.
    /// 아이템을 하나씩 드롭 합니다.
    /// </summary>
    void RButtonDownOnDrag()
    {
        if (Input.GetMouseButtonDown(1) && isDrag)
        {
            // 마우스 위치에 있는 UI 요소를 가져옵니다.
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // 마우스 위치에 다른 슬롯이 있는지 확인합니다.
            foreach (RaycastResult result in results)
            {
                Slot otherSlot = result.gameObject.GetComponent<Slot>();
                if (otherSlot != null && otherSlot != this)
                {
                    // 다른 슬롯에 아이템을 드롭합니다.
                    AddItem(otherSlot, this.item);
                }
            }
        }
    }

    /// <summary>
    /// 드래그 시작때 불러지는 함수입니다.
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
    /// 드래그 중에 불러지는 함수입니다.
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
    /// 드래그 종료 시 불러지는 함수입니다.
    /// 드래그가 종료된 위치의 슬롯과 아이템을 교환합니다.
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isHaveItem)
        {
            // 마우스 위치에 있는 UI 요소를 가져옵니다.
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // 마우스 위치에 다른 슬롯이 있는지 확인합니다.
            foreach (RaycastResult result in results)
            {
                Slot otherSlot = result.gameObject.GetComponent<Slot>();
                if (otherSlot != null && otherSlot != this)
                {
                    // 다른 슬롯과 아이템을 교환합니다.
                    SwapItems(otherSlot);
                }
            }

            transform.position = startPosition;

            // 이 슬롯이 완성품일경우, 슬롯을 교환 후 제작대의 아이템을 삭제합니다.
            if(!isHaveItem && this.type == (int)SlotType.MANUFACTURED)
            {
                if (cTable) cTable.RemoveCraftingSlots();
                else if (iTable) iTable.RemoveCraftingSlots();
            }
        }
        isDrag = false;
    }

    /// <summary>
    /// 특정 슬롯에 아이템을 추가하는 함수입니다.
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
    /// 이 슬롯에 아이템을 추가하는 함수입니다.
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
    /// 이 슬롯의 아이템을 삭제하는 함수입니다.
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
    /// 다른 슬롯과 아이템을 교환하는 함수입니다.
    /// </summary>
    void SwapItems(Slot otherSlot)
    {
        if (otherSlot.type != (int)SlotType.MANUFACTURED)
        {
            // 이 슬롯과 다른 슬롯의 아이템을 교환합니다.
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

            // UI를 업데이트합니다.
            UpdateItemImage();
            otherSlot.UpdateItemImage();
        }
    }

    void Awake()
    {
        image = GetComponent<Image>();
        
        cTable = GetComponentInParent<CratftTableUI>();
        iTable = GetComponentInParent<InventoryUI>();


        tooltip = transform.Find("tooltip").gameObject;
        tooltipText = GetComponentInChildren<TextMeshProUGUI>();
        tooltip.SetActive(false);

        isHaveItem = false;
        UpdateItemImage();
    }

    /// <summary>
    /// 슬롯의 이미지를 업데이트하는 함수입니다.
    /// </summary>
    public void UpdateItemImage()
    {
        //아이템을 갖고 있을 때 아이템이미지를 내보냅니다.
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
                Debug.LogError("이미지를 찾을 수 없습니다: " + item.name);
            }
        }
        //아이템이 없을을 때 아이템이미지를 내보냅니다.
        // 현재는 Magenta를 출력합니다.
        else
        {
            image.sprite = null;
            Color tempColor = image.color;
            tempColor.a = 0.0f;        // + 빌드시 투명으로 바꿉니다.
            tempColor.r = 1.0f;
            tempColor.g = 0f;
            tempColor.b = 1.0f;
            image.color = tempColor;
        }

        // 제작대 슬롯이 업데이트 될 때 마다, 제작대를 확인합니다.
        if (this.type == (int)SlotType.CRAFT)
        {
            if (cTable) cTable.CheckCraftTable();
            else if (iTable) iTable.CheckCraftTable();
        }
    }

    /// <summary>
    /// 슬롯에 있는 아이템의 번호를 가져오는 함수입니다.
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
    /// 슬롯을 우클릭 시 불러지는 함수입니다.
    /// 드래그중이 아닐 때 우클릭시 슬롯에 있는 아이템을 삭제합니다.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !isDrag)
        {
            RemoveItem(this);
        }
    }

    //툴팁
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isHaveItem && !isDrag)
        {
            tooltip.SetActive(true);    //툴팁 ON
            tooltipText = GetComponentInChildren<TextMeshProUGUI>();
            tooltipText.text = "ITEM : " + item.imageName;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);   //툴팁 OFF
    }
}
