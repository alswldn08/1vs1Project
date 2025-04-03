using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // 에디터에서 할당할 아이템 데이터

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 아이템을 먹으면
        {
            InventoryManager.instance.AddItem(item);
            Destroy(gameObject); // 아이템 제거
        }
    }
}
