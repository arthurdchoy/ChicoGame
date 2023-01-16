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

    public Sprite[] fieldSprites;

    [SerializeField]
    public GameObject plantEntity;

    // Different field tile flags:
    private enum TileFlags { BLOCKED, TILLED, WATERED, SOWED };
    private Dictionary<Vector3Int, List<bool>> fieldTiles = new Dictionary<Vector3Int, List<bool>>();
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
                fieldTiles.Add(gridPosition, new List<bool> { false, false, false, false });
            }

            Debug.Log("Attempting to use " + item.ItemName);

            if (item.ItemName == "Hoe" && empty(gridPosition)) HoeInteraction(gridPosition);
            else if (item.ItemName == "Axe") AxeInteraction(gridPosition);
            else if (item.ItemName == "Hammer" && !(watered(gridPosition) || sowed(gridPosition))) HammerInteraction(gridPosition);
            else if (item.ItemName == "Shovel" && sowed(gridPosition)) ShovelInteraction(gridPosition);
            else if (item.ItemName == "Watering Can" && tilled(gridPosition)) WateringCanInteraction(gridPosition);
            else if (item is SeedData && tilled(gridPosition) && !sowed(gridPosition)) SeedInteraction(gridPosition, (SeedData)item);



            if (empty(gridPosition))
            {
                fieldTiles.Remove(gridPosition);
            }
        }
    }

    private void HammerInteraction(Vector3Int gridPosition)
    {
        Debug.Log("hammer interaction");
        if (blocked(gridPosition)) unblock(gridPosition);
        if(tilled(gridPosition))
        {
            dry(gridPosition);
            flatten(gridPosition);
        }
    }

    private void AxeInteraction(Vector3Int gridPosition)
    {
        unblock(gridPosition);
    }

    private void HandsInteraction(Vector3Int gridPosition)
    {
    }

    private void HoeInteraction(Vector3Int gridPosition)
    {
        Debug.Log("hoe interaction");
        till(gridPosition);
    }

    private void ShovelInteraction(Vector3Int gridPosition)
    {
        reap(gridPosition);
        if(plants.ContainsKey(gridPosition))
        {
            Destroy(plants[gridPosition], 0.0f);
            plants.Remove(gridPosition);
        }

        dry(gridPosition);
    }

    private void WateringCanInteraction(Vector3Int gridPosition)
    {
        Debug.Log("watering");
        water(gridPosition);
    }

    private void SeedInteraction(Vector3Int gridPosition, SeedData seed)
    {
        Debug.Log("Seed interaction");
        sow(gridPosition);
        GameObject instance = Instantiate(plantEntity, interactionCursor.transform.position, Quaternion.identity);

        StartCoroutine(instance.GetComponent<PlantEntity>().StartGrowing(dayNightManager.days, seed.growthStageTimes, seed.growthStageSprites, instance.GetComponent<SpriteRenderer>()));

        if(!plants.ContainsKey(gridPosition)) plants.Add(gridPosition, instance);
    }

    /*
     * FIELD FLAG ACCESSORS
     */
    bool blocked(Vector3Int gridPosition)
    {
        if (!fieldTiles.ContainsKey(gridPosition)) return false;
        return fieldTiles[gridPosition][(int)TileFlags.BLOCKED];
    }

    bool tilled(Vector3Int gridPosition)
    {
        if (!fieldTiles.ContainsKey(gridPosition)) return false;
        return fieldTiles[gridPosition][(int)TileFlags.TILLED];
    }

    bool watered(Vector3Int gridPosition)
    {
        if (!fieldTiles.ContainsKey(gridPosition)) return false;
        return fieldTiles[gridPosition][(int)TileFlags.WATERED];
    }

    bool sowed(Vector3Int gridPosition)
    {
        if (!fieldTiles.ContainsKey(gridPosition)) return false;
        return fieldTiles[gridPosition][(int)TileFlags.SOWED];
    }

    bool empty(Vector3Int gridPosition)
    {
        if (!fieldTiles.ContainsKey(gridPosition)) return true;
        return !(blocked(gridPosition) ||
                 tilled(gridPosition)  ||
                 watered(gridPosition) ||
                 sowed(gridPosition));
    }

    /*
     * FIELD FLAG MODIFIERS
     */
    // BLOCKED FLAG
    void block(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.BLOCKED] = true;
    }
    void unblock(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.BLOCKED] = false;
    }
    // TILLED FLAG
    void till(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.TILLED] = true;
    }
    void flatten(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.TILLED] = false;
    }
    // WATERED FLAG
    void water(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.WATERED] = true;
    }
    void dry(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.WATERED] = false;
    }
    // SOWED FLAG
    void sow(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.SOWED] = true;
    }
    void reap(Vector3Int gridPosition)
    {
        fieldTiles[gridPosition][(int)TileFlags.SOWED] = false;
    }
}
