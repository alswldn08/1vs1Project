using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryPanel; // 아이템을 표시할 패널
    public GameObject itemSlotPrefab; // 아이템 슬롯 프리팹

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject); // 기존 아이템 슬롯 제거
        }

        foreach (Item item in InventoryManager.instance.inventory)
        {
            GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel);
            slot.transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon; // 아이콘 설정
            slot.transform.GetChild(1).GetComponent<Text>().text = item.itemName; // 이름 설정
        }
    }
}
