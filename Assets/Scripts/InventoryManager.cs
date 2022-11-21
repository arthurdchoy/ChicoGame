using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int NUM_SLOTS = 15;
    private int count = 0;     // number of used inventory slots
    
    // pos of item in list corresponds to their pos in inventory UI
    private List<(InventoryItem item, int count)> itemList = new List<(InventoryItem item, int count)>(NUM_SLOTS);

    void Start()
    {
        // for inserting items, inventory drap&drop feature
        for(int i = 0; i < NUM_SLOTS; i++)
        {
            itemList.Add((null, 0));
        }
    }
    
    // return if item was added succesfully
    public bool AddItem(InventoryItem newItem, int amount = 1)
    {
        if (FullInventory())
        {
            return false;
        }

        int firstNullIndex = -1;
        bool nullIndexFound = false;
        for (int i = 0; i < NUM_SLOTS; i++)
        {
            if (itemList[i].item == null)
            {
                if (!nullIndexFound)
                {
                    firstNullIndex = i;
                    nullIndexFound = true;
                }
                continue;
            }

            if (itemList[i].item.name == newItem.name && newItem.stackable)
            {
                int newAmount = itemList[i].count + amount;
                itemList[i] = (newItem, newAmount);
                count++;
                return true;
            }
        }
        itemList[firstNullIndex] = (newItem, amount);
        count++;
        return true;
    }

    // insert item at specific inventory slot (for drap&drop)
    // return false if item cant be added
    public bool AddItem(InventoryItem newItem, int amount, int index)
    {
        if(itemList[index].item != null)
        {
            if(itemList[index].item.name == newItem.name && itemList[index].item.stackable)
            {
                int newAmount = itemList[index].count + amount;
                itemList[index] = (newItem, newAmount);
                count++;
                return true;
            }
            return false;
        }
        itemList[index] = (newItem, amount);
        count++;
        return true;
    }

    // return true if item is removed by appropriate amount
    // amount is -1 when the full amount of the item at the index should be removed
    public bool RemoveItem(int index, int amount = -1)
    {
        if(itemList[index].item == null && itemList[index].count < amount && amount != -1)
        {
            return false;
        }
        if(itemList[index].count == amount || amount == -1)
        {
            itemList[index] = (null, 0);
            count--;
            return true;
        }
        int newAmount = itemList[index].count - amount;
        itemList[index] = (itemList[index].item, newAmount);
        return true;

    }
    
    bool FullInventory()
    {
        return (count >= NUM_SLOTS);
    }
    bool EmptyInventory()
    {
        return (count == 0);
    }
}
