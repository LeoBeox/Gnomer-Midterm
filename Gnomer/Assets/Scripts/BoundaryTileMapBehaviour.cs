using System;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoundaryTileMapBehaviour : MonoBehaviour
{


    public Tilemap tilemap;
    public GameObject tilePrefab;

    private Dictionary<Vector3Int, GameObject> tilePrefabs = new Dictionary<Vector3Int, GameObject>();




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            // convert grid position to world position
            Vector3 worldPos = tilemap.CellToWorld(pos) + tilemap.tileAnchor;

            // spawn prefab at tile position
            GameObject prefabInstance = Instantiate(tilePrefab, worldPos, Quaternion.identity);

            tilePrefabs[pos] = prefabInstance;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
