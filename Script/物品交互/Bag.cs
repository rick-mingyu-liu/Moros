using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// By Rick

public class Inventory : MonoBehaviour
{
    // Define a list to hold items in the inventory
    private List<GameObject> inventoryItems = new List<GameObject>();

    // Adds an item to the inventory
    public void AddItem(GameObject item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
            item.SetActive(false); // Hide the item in the scene when picked up
            Debug.Log(item.name + " has been added to the inventory.");
        }
        else
        {
            Debug.Log("Item is already in the inventory.");
        }
    }

    // Removes an item from the inventory
    public void RemoveItem(GameObject item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            Debug.Log(item.name + " has been removed from the inventory.");
        }
        else
        {
            Debug.Log("Item is not in the inventory.");
        }
    }

    // Checks if an item exists in the inventory
    public bool HasItem(GameObject item)
    {
        return inventoryItems.Contains(item);
    }

    // Use an item in the inventory
    public void UseItem(GameObject item, Transform usePosition)
    {
        if (HasItem(item))
        {
            item.transform.position = usePosition.position; // Place the item in the scene
            item.SetActive(true); // Make the item visible again
            Debug.Log(item.name + " has been used.");
        }
        else
        {
            Debug.Log("You do not have this item in your inventory.");
        }
    }
}
