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
    private GameObject _CraftUI;
    public List<Item> items = new List<Item>();  //아이템들이 저장되는 리스트입니다.

    public Button addBasicItemButton;   //기본아이템을 추가하는 버튼입니다.

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
    public void DisplayItemToUI()
    {
        foreach (var item in items)
        {
            CratftTableUI ui = _CraftUI.GetComponent<CratftTableUI>();
            ui.AddItemToSlot(item);
        }
    }

    /// <summary>
    /// 아이템 리스트를 가져오는 함수입니다.
    /// </summary>
    public List<Item> GetItemList()
    {
        return items;
    }

    /// <summary> TODO) 아이템을 얻거나 제작할때마다 리스트를 변경해야합니다.
    /// 현재는 리스트에 추가해서 쓸 기능이 없으므로 비워둡니다.
    /// </summary>
    public void AddItemToList(Item item)
    {

    }

}