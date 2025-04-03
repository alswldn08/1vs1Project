using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // 싱글턴 패턴 적용
    public List<Item> inventory = new List<Item>(); // 인벤토리 리스트

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // 아이템 추가
    public void AddItem(Item newItem)
    {
        inventory.Add(newItem);
        Debug.Log(newItem.itemName + "을(를) 획득했습니다!");
    }

    // 아이템 제거
    public void RemoveItem(Item item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log(item.itemName + "을(를) 버렸습니다.");
        }
    }
}
