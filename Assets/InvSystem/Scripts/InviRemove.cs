using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemScript : MonoBehaviour
{
    public Item item;

    public void RemoveItemFromInventory()
    {
        InventoryManager.Instance.Remove(item);
    }
}
