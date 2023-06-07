using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap fieldMap;
    [SerializeField]
    GameObject interactionCursor;
    [SerializeField]
    DayNightManager dayNightManager;
    [SerializeField]
    InventoryManager playerInventory;

    public Sprite[] fieldSprites;

    [SerializeField]
    public GameObject plantEntity;

    // Different field tile flags:
    private enum TileStates 
    { 
        STONEBLOCKED, 
        WOODBLOCKED,
        EMPTY,
        TILLED,
        WATEREDNOPLANT,
        UNWATEREDPLANT,
        WATEREDPLANT
    };
    private Dictionary<Vector3Int, TileStates> fieldTiles = new Dictionary<Vector3Int, TileStates>();
    private Dictionary<Vector3Int, GameObject> plants     = new Dictionary<Vector3Int, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*foreach(KeyValuePair<Vector3Int, List<bool>> tiles in fieldTiles)
        {
            
        }*/
    }

    public void Interact(InventoryItem item, Vector3Int gridPosition)
    {
        if (fieldMap.GetTile(gridPosition))
        {
            if (!fieldTiles.ContainsKey(gridPosition))
            {
                fieldTiles.Add(gridPosition, TileStates.EMPTY);
            }

            Debug.Log("Attempting to use " + item.itemName);

            fieldTiles[gridPosition] = TileInteractionUpdate(fieldTiles[gridPosition], item, gridPosition);
        }
    }

    private TileStates TileInteractionUpdate(TileStates state, InventoryItem item, Vector3Int gridPosition)
    {
        ToolType tempToolType = ToolType.INVALID;
        SeedData tempSeed = null;
        if (item is ToolData temp1) { tempToolType = temp1.toolType; }
        if (item is SeedData temp2) { tempSeed = temp2; }
        // State Transitions
        switch(state)
        {
            case TileStates.STONEBLOCKED:
                if(tempToolType == ToolType.HAMMER)
                {
                    state = TileStates.EMPTY;
                }
                break;
            case TileStates.WOODBLOCKED:
                if(tempToolType == ToolType.AXE)
                {
                    state = TileStates.EMPTY;
                }
                break;
            case TileStates.EMPTY:
                if(tempToolType == ToolType.HOE)
                {
                    state = TileStates.TILLED;
                }
                break;
            case TileStates.TILLED:
                if(tempToolType == ToolType.HAMMER)
                {
                    state = TileStates.EMPTY;
                }
                else if(tempToolType == ToolType.WATERINGCAN)
                {
                    state = TileStates.WATEREDNOPLANT;
                }
                if(tempSeed != null)
                {
                    state = TileStates.UNWATEREDPLANT;
                    
                    GameObject instance = Instantiate(plantEntity, interactionCursor.transform.position, Quaternion.identity);
                    instance.GetComponent<PlantEntity>().Initialize(tempSeed);
                    plants.Add(gridPosition, instance);
                }
                break;
            case TileStates.WATEREDNOPLANT:
                if(tempToolType == ToolType.HAMMER)
                {
                    state = TileStates.EMPTY;
                }
                if(tempSeed != null)
                {
                    state = TileStates.WATEREDPLANT;

                    GameObject instance = Instantiate(plantEntity, interactionCursor.transform.position, Quaternion.identity);
                    instance.GetComponent<PlantEntity>().Initialize(tempSeed);
                    plants.Add(gridPosition, instance);
                }
                break;
            case TileStates.UNWATEREDPLANT:
                if(tempToolType == ToolType.HANDS)
                {
                    PlantEntity tempPlant = plants[gridPosition].GetComponent<PlantEntity>();
                    if(tempPlant.growthStage == tempPlant.seed.maxGrowthStage)
                    {
                        playerInventory.AddItem(tempPlant.seed.crop, tempPlant.seed.harvestAmount);
                        plants.Remove(gridPosition);
                        state = TileStates.EMPTY;
                    }
                }
                if(tempToolType == ToolType.SHOVEL)
                {
                    PlantEntity tempPlant = plants[gridPosition].GetComponent<PlantEntity>();
                    if(tempPlant.growthStage == tempPlant.seed.maxGrowthStage)
                    {
                        playerInventory.AddItem(tempPlant.seed.crop, tempPlant.seed.harvestAmount);
                        plants.Remove(gridPosition);
                    }
                    else
                    {
                        playerInventory.AddItem(tempPlant.seed, 1);
                        plants.Remove(gridPosition);
                    }
                    state = TileStates.EMPTY;
                }
                if(tempToolType == ToolType.WATERINGCAN)
                {
                    state = TileStates.WATEREDPLANT;
                }
                break;
            case TileStates.WATEREDPLANT:
                if (tempToolType == ToolType.HANDS)
                {
                    PlantEntity tempPlant = plants[gridPosition].GetComponent<PlantEntity>();
                    if (tempPlant.growthStage == tempPlant.seed.maxGrowthStage)
                    {
                        playerInventory.AddItem(tempPlant.seed.crop, tempPlant.seed.harvestAmount);
                        plants.Remove(gridPosition);
                        state = TileStates.EMPTY;
                    }
                }
                if (tempToolType == ToolType.SHOVEL)
                {
                    PlantEntity tempPlant = plants[gridPosition].GetComponent<PlantEntity>();
                    if (tempPlant.growthStage == tempPlant.seed.maxGrowthStage)
                    {
                        playerInventory.AddItem(tempPlant.seed.crop, tempPlant.seed.harvestAmount);
                        plants.Remove(gridPosition);
                    }
                    else
                    {
                        playerInventory.AddItem(tempPlant.seed, 1);
                        plants.Remove(gridPosition);
                    }
                    state = TileStates.EMPTY;
                }
                break;
        }
        return state;
    } 

    public void DayPassed()
    {
        foreach(KeyValuePair<Vector3Int, GameObject> i in plants)
        {
            GameObject temp = i.Value;
            temp.GetComponent<PlantEntity>().Grow(temp.GetComponent<SpriteRenderer>());
        }
    }
}
