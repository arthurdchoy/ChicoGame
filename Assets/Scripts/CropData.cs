using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Item/New Crop")]

public class CropData : InventoryItem
{
    private void Awake()
    {
        maxStackSize = 64;
        itemType = ItemType.CROP;
    }

    public int buyPrice;
    public int sellPrice;

    public PlantType plantType;
    public PlantSpecies plantSpecies;

    //public string[] crossbreed;
}
