using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    void PickUp()
    {
        InventoryManager.Instance.Add(item);
    }
    private void OnDestroy()
    {
        PickUp();
    }
}
