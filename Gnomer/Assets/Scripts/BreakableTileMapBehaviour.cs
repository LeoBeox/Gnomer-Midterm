using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTileMapBehaviour : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject tilePrefab;
    private TilemapCollider2D _tmCollider;

    public int maxTileHealth;

    // Track health for each tile
    private Dictionary<Vector3Int, int> tileHealth = new Dictionary<Vector3Int, int>();
    
    // Track prefab instances for each tile
    private Dictionary<Vector3Int, GameObject> tilePrefabs = new Dictionary<Vector3Int, GameObject>();

    void Start()
    {

        _tmCollider = GetComponent<TilemapCollider2D>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            tileHealth[pos] = maxTileHealth;

            // convert grid position to world position
            Vector3 worldPos = tilemap.CellToWorld(pos) + tilemap.tileAnchor;

            // spawn prefab at tile position
            GameObject prefabInstance = Instantiate(tilePrefab, worldPos, Quaternion.identity);

            tilePrefabs[pos] = prefabInstance;
        }

    }

    // Handles collision with player and breaking of tile
    void OnCollisionEnter2D(Collision2D other)
    {

        Vector2 hitPosition = other.contacts[0].point;
        
        Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

        if (other.gameObject.CompareTag("Player"))
        {

            // Debug.Log("enterd if statement");


            TileBehaviour tileBehaviour = tilePrefabs[cellPosition].GetComponent<TileBehaviour>();
            if (tileHealth.ContainsKey(cellPosition))
            {
                tileHealth[cellPosition]--;

                tileBehaviour.isDamaged();

                // Debug.Log("enterd if 2. Tile health: " + tileHealth[cellPosition]);

                if (tileHealth[cellPosition] <= 0)
                {
                    // Debug.Log("enterd if 3");
                    BreakTile(cellPosition);
                }
            }
            
        }


    }

    // Breaks tile only at cell position 
    private void BreakTile(Vector3Int cellPosition)
    {
        
        // Sets tile to empty
        tilemap.SetTile(cellPosition, null);

        // Destroy the prefab instance
        if (tilePrefabs.ContainsKey(cellPosition))
        {

            // Debug.Log("enterd first if breaktile");

            TileBehaviour tileBehaviour = tilePrefabs[cellPosition].GetComponent<TileBehaviour>();
            if (tileBehaviour != null)
            {
                // Debug.Log("enterd secondif breaktile");
                // tileBehaviour.PlayBreakEffect();
            }

            Destroy(tilePrefabs[cellPosition]);
            tilePrefabs.Remove(cellPosition);
        }

        // Remove from health tracking
        tileHealth.Remove(cellPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
