using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class InteractionManager : MonoBehaviour
{
    // Player character and cursor object for calculation and visuals
    [SerializeField] GameObject player, cursor, field;
    public Tilemap tiles;   // Tilemap object for function calls
    public Tile tile;       // Object for currently selected tile

    public Vector3Int location; // Selection location
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player position and find selection spot based on player's facing direction
        Vector3Int playerPos = tiles.WorldToCell(player.transform.position);
        switch(player.GetComponent<CharacterController2D>().GetFacing())
        {
            case CharacterController2D.dir.UP:
                location = playerPos + Vector3Int.up;
                break;
            case CharacterController2D.dir.DOWN:
                location = playerPos + Vector3Int.down;
                break;
            case CharacterController2D.dir.RIGHT:
                location = playerPos + Vector3Int.right;
                break;
            case CharacterController2D.dir.LEFT:
                location = playerPos + Vector3Int.left;
                break;
        }

        // Place cursor onto selection location
        // (other additions to offset location to be in middle of tile, it's awful I know)
        Vector3 cursorXY = tiles.CellToWorld(location) + tiles.CellToWorld(Vector3Int.down)/2 + tiles.CellToWorld(Vector3Int.right)/2;
        cursor.transform.position = new Vector3(cursorXY.x, cursorXY.y, -0.2f);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            field.GetComponent<FieldManager>().Interact(null, location);
        }
    }
}
