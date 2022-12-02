using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int NUM_SLOTS = 15;
    private int count = 0;     // number of used inventory slots
    
    // pos of item in list corresponds to their pos in inventory UI
    private List<(IInventoryItem item, int amount)> itemList = new List<(IInventoryItem item, int amount)>(NUM_SLOTS);

    public ScriptableObject[] startingItems;

    void Start()
    {
        if (startingItems.Length > 0) {
            for(int i = 0; i < startingItems.Length; i++)
            {
                if (startingItems[i] is ToolData) AddItem((IInventoryItem)startingItems[i], 1, false);
                else if (startingItems[i] is SeedData) AddItem((IInventoryItem)startingItems[i], 64, false);
                else if (startingItems[i] is CropData) AddItem((IInventoryItem)startingItems[i], 64, false);
            }
        }
        // for inserting items, inventory drag&drop feature
        for(int i = 0; i < NUM_SLOTS; i++)
        {
            itemList.Add((null, 0));
        }
    }

    public bool AddItem(IInventoryItem newItem, int amount, bool ugh)
    {
        Debug.Log("I am at my lowest point");
        itemList.Add((newItem, amount));
        count++;
        return true;
    }
    
    // return if item was added succesfully
    public bool AddItem(IInventoryItem newItem, int amount)
    {
        if (FullInventory())
        {
            return false;
        }

        int firstNullIndex = -1;
        bool nullIndexFound = false;
        for (int i = 0; i < NUM_SLOTS; i++)
        {
            Debug.Log(count);
            if (itemList.Count == 0 || itemList[i].item == null)
            {
                if (!nullIndexFound)
                {
                    firstNullIndex = i;
                    nullIndexFound = true;
                }
                continue;
            }

            if (itemList[i].item.ItemName == newItem.ItemName && newItem.MaxStackSize > 1)
            {
                int newAmount = itemList[i].amount + amount;
                itemList[i] = (newItem, newAmount);
                count++;
                return true;
            }
        }
        itemList.Add((newItem, amount));
        count++;
        return true;
    }

    // insert item at specific inventory slot (for drap&drop)
    // return false if item cant be added
    public bool AddItem(IInventoryItem newItem, int amount, int index)
    {
        Debug.Log(index);
        if(itemList[index].item != null)
        {
            
            if(itemList[index].item.ItemName == newItem.ItemName && itemList[index].item.MaxStackSize > 1)
            {
                int newAmount = itemList[index].amount + amount;
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
        if(itemList[index].item == null && itemList[index].amount < amount && amount != -1)
        {
            return false;
        }
        if(itemList[index].amount == amount || amount == -1)
        {
            itemList[index] = (null, 0);
            count--;
            return true;
        }
        int newAmount = itemList[index].amount - amount;
        itemList[index] = (itemList[index].item, newAmount);
        return true;

    }

    public (IInventoryItem item, int amount) GetItemAt(int index)
    {
        return itemList[index];
    }

    public int GetNumFilledSlots()
    {
        return count;
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
