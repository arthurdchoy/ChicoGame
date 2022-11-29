using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap fieldMap;

    // Different field tile flags:
    private enum TileFlags { BLOCKED, TILLED, WATERED, SOWED };
    private Dictionary<Vector3Int, List<bool>> fieldTiles = new Dictionary<Vector3Int, List<bool>>();
    private Dictionary<Vector3Int, bool> tilledTiles  = new Dictionary<Vector3Int, bool>();
    private Dictionary<Vector3Int, bool> wateredTiles = new Dictionary<Vector3Int, bool>();
    private Dictionary<Vector3Int, bool> sowedTiles   = new Dictionary<Vector3Int, bool>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(InventoryItem item, Vector3Int gridPosition)
    {
        if (fieldMap.GetTile(gridPosition))
        {
            if (!fieldTiles.ContainsKey(gridPosition))
            {
                fieldTiles.Add(gridPosition, new List<bool> { false, false, false, false });
            }

            if (blocked(gridPosition))
            {
                fieldTiles[gridPosition][(int)TileFlags.BLOCKED] = false;
                Debug.Log("Unblocked field " + fieldMap.GetTile(gridPosition));
            }
            else if (empty(gridPosition))
            {
                fieldTiles[gridPosition][(int)TileFlags.TILLED] = true;
                Debug.Log("Tilled field " + fieldMap.GetTile(gridPosition));
            }
            else if (tilled(gridPosition))
            {
                fieldTiles[gridPosition][(int)TileFlags.WATERED] = true;
                Debug.Log("Watered field " + fieldMap.GetTile(gridPosition));
            }
            else if (watered(gridPosition))
            {
                fieldTiles[gridPosition][(int)TileFlags.SOWED] = true;
                Debug.Log("Sowed field " + fieldMap.GetTile(gridPosition));
            }

            if (empty(gridPosition))
            {
                fieldTiles.Remove(gridPosition);
            }
        }
    }

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
}
