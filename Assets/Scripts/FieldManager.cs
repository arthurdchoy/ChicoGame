using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap fieldMap;

    private Dictionary<Vector3Int, int> fieldTiles = new Dictionary<Vector3Int, int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
