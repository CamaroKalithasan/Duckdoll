using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private List<Item> inventory;

    private void Start()
    {
        inventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        // Perform any additional logic, such as updating UI or triggering events
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        // Perform any additional logic, such as updating UI or triggering events
    }

    // Other methods for interacting with the inventory (e.g., use item, drop item, etc.)

    public bool ContainsItem(Item item)
    {
        return inventory.Contains(item);
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }
}

public class Item
{
    public string itemName;
    public Sprite icon;

    public Item(string name, Sprite sprite)
    {
        Name = name;
        Sprite = sprite;
    }

    public string Name { get; }
    public Sprite Sprite { get; }
    // Additional properties and methods for the item
}
