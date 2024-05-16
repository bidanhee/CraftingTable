using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 플레이어 클래스 입니다.
/// 보유한 아이템들을 추가 및 저장하기 위해 사용됩니다.
/// </summary>
public class Player : MonoBehaviour
{
    public GameObject CraftUI;
    private GameObject _CraftUI;
    public List<Item> items = new List<Item>();  //아이템들이 저장되는 리스트입니다.

    private void Start()
    {
        AddBasicItem();
        _CraftUI = Instantiate(CraftUI, transform);
        _CraftUI.SetActive(true);
        DisplayItemToUI();
    }
    
    public void AddBasicItem()
    {
        Stick stick= new Stick("막대기", "Stick");
        String _string = new String("실", "String");
        Iron iron = new Iron("철", "Iron");

        items.Add(stick);
        items.Add(_string);
        items.Add(iron);
    }

    public void DisplayItemToUI()
    {
        foreach (var item in items)
        {
            CratftTableUI ui = _CraftUI.GetComponent<CratftTableUI>();
            ui.AddItemToSlot(item);
        }
    }

    public List<Item> GetItemList()
    {
        return items;
    }

    //TODO) 아이템을 얻거나 제작할때마다 리스트를 변경해야합니다.
    //현재는 리스트에 추가해서 쓸 기능이 없으므로 비워둡니다.
    public void AddItemToList(Item item)
    {

    }

}