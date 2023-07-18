using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public GameObject inventoryItemPrefab;
    public Transform inventoryContent;

    private InventorySystem inventorySystem;
    private bool isInventoryOpen;

    private void Start()
    {
        inventorySystem = GetComponent<InventorySystem>();
        isInventoryOpen = false;
        UpdateInventoryUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryOpen = !isInventoryOpen;
            UpdateInventoryUI();
        }
    }

    private void UpdateInventoryUI()
    {
        ClearInventoryUI();

        if (isInventoryOpen)
        {
            List<Item> inventory = inventorySystem.GetInventory();

            foreach (Item item in inventory)
            {
                GameObject itemUI = Instantiate(inventoryItemPrefab, inventoryContent);
                Image iconImage = itemUI.GetComponent<Image>();
                Text itemNameText = itemUI.GetComponentInChildren<Text>();

                iconImage.sprite = item.icon;
                itemNameText.text = item.Name;
            }
        }
    }

    private void ClearInventoryUI()
    {
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }
    }
}
