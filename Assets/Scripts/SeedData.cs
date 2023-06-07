using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SeedData", menuName = "Item/New Seed")]

public class SeedData : InventoryItem
{
    private void Awake()
    {
        maxStackSize = 64;
        itemType = ItemType.SEED;
    }
    public bool reharvestable;

    public int buyPrice;
    public int sellPrice;

    public int harvestAmount;
    public int maxGrowthStage;
    public int reharvestGrowthStage;
    public int[] growthStageTimes;
    public Sprite[] growthStageSprites;

    public CropData crop;
    public PlantType plantType;
    public PlantSpecies plantSpecies;
}
