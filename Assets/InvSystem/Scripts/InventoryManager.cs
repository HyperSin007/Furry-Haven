using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems();
    }

    public void Remove(Item item)
    {
        int index = Items.FindIndex(i => i == item);
        if (index != -1)
        {
            Items.RemoveAt(index);
            ListItems();
        }
    }

    public void ListItems()
    {
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            if (itemIcon != null && item.icon != null)
            {
                itemIcon.sprite = item.icon;
            }
            else
            {
                Debug.LogError("Failed to find Image component or sprite is null for item: " + item.itemName);
            }

            Button removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            if (removeButton != null)
            {
                removeButton.onClick.RemoveAllListeners();
                removeButton.onClick.AddListener(() => Remove(item));
            }
        }
    }
}