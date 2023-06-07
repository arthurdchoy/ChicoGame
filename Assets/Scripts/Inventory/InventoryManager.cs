using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int NUM_SLOTS = 15;
    private int slotsUsed = 0;     // number of used inventory slots
    
    // pos of item in list corresponds to their pos in inventory UI
    private List<(InventoryItem item, int amount)> itemList = new List<(InventoryItem item, int amount)>(NUM_SLOTS);

    void Awake()
    {
        // for inserting items, inventory drag&drop feature
        fillEmptyList(NUM_SLOTS);

    }
    
    public void fillEmptyList(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            itemList.Add((null, 0));
        }
    }

    // return true if item was added succesfully, false otherwise   (also return false if some stackable items are left out due to full inventory)
    public bool AddItem(InventoryItem newItem, int amount)
    {
        // exit immediately and return false if inventory full
        if (FullInventory())
        {
            return false;
        }

        int firstEmptySlotIndex = GetFirstEmptySlot();

        // if item is stackable, find the first available slot, if none exist, add item to first empty slot
        if (newItem.maxStackSize > 1)
        {
            for (int i = 0; i < NUM_SLOTS; i++)
            {
                // slot if available if items are the same are the same AND the item currently in slot is not at maximum stack size
                if (itemList[i].item == newItem && itemList[i].amount < itemList[i].item.maxStackSize)
                {
                    // if adding the extra amount exceeds stack size, recursively call AddItem with the remaining amount
                    if (itemList[i].amount + amount > itemList[i].item.maxStackSize)
                    {
                        itemList[i] = (newItem, itemList[i].item.maxStackSize);
                        return AddItem(newItem, amount - (itemList[i].item.maxStackSize - itemList[i].amount));
                    }
                    else
                    {
                        itemList[i] = (newItem, itemList[i].amount + amount);
                        return true;
                    }
                }
            }
        }

        // item not stackable OR no available, stackable slot --> add to first empty slot
        // if adding amount exceeds stack size, recursively call AddItem with the remaining amount
        if(amount > newItem.maxStackSize)
        {
            itemList[firstEmptySlotIndex] = (newItem, newItem.maxStackSize);
            slotsUsed++;
            return AddItem(newItem, amount - newItem.maxStackSize);
        }
        else
        {
            itemList[firstEmptySlotIndex] = (newItem, amount);
            slotsUsed++;
            return true;
        }

    }

    // return true if item is removed by appropriate amount
    // amount is -1 when the full amount of the item at the index should be removed
    public bool RemoveItem(int index, int amount = -1)
    {
        // immediately return false if there is nothing at the slot position 
        if(itemList[index].item == null)
        {
            return false;
        }

        // clear the entire inventory slot if amount == -1 or the amount to remove is greater than the current amount
        if(amount >= itemList[index].amount || amount == -1)
        {
            itemList[index] = (null, 0);
            slotsUsed--;
            return true;
        }

        itemList[index] = (itemList[index].item, itemList[index].amount - amount);
        return true;
    }

    // return true if item is removed by appropriate amount
    // amount = 1 by default
    public bool RemoveItem(InventoryItem item, int amount = 1)
    {
        int amountRemoved;
        for (int i = 0; i < NUM_SLOTS; i++)
        {
            if (itemList[i].item == item)
            {
                if (amount < itemList[i].amount)
                {
                    itemList[i] = (item, itemList[i].amount - amount);
                    return true;
                }
                else
                {
                    amountRemoved = itemList[i].amount;
                    itemList[i] = (null, 0);
                    slotsUsed--;
                    return RemoveItem(item, amount - amountRemoved);
                }

            }
        }
        return false;
    }

    
    public InventoryItem GetItemAt(int index)
    {
        return itemList[index].item;
    }

    public int GetAmountAt(int index)
    {
        return itemList[index].amount;
    }

    public int GetNumFilledSlots()
    {
        return slotsUsed;
    }
    
    bool FullInventory()
    {
        return (slotsUsed >= NUM_SLOTS);
    }
    bool EmptyInventory()
    {
        return (slotsUsed == 0);
    }

    // return the first empty slot in itemList, return -1 if no empty slot found
    private int GetFirstEmptySlot()
    {
        int firstEmptySlot = -1;
        for (int i = 0; i < NUM_SLOTS; i++)
        {
            if (itemList[i].item == null)
            {
                firstEmptySlot = i;
                return firstEmptySlot;
            }
        }
        return firstEmptySlot;
    }

    private void LogInventory()
    {
        for(int i = 0; i < NUM_SLOTS; i++)
        {
            if(itemList[i].item == null)
            {
                Debug.Log("NaN," + 0);
            }
            else
            {
                Debug.Log(itemList[i].item.itemName + "," + itemList[i].amount);
            }
        }
    }
}
