using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTileMapBehaviour : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject tilePrefab;

    public int maxTileHealth = 2; 

    // Track health for each tile
    private Dictionary<Vector3Int, int> tileHealth = new Dictionary<Vector3Int, int>();

    // Track prefab instances for each tile
    private Dictionary<Vector3Int, GameObject> tilePrefabs = new Dictionary<Vector3Int, GameObject>();

    void Start()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            tileHealth[pos] = maxTileHealth;

            Vector3 worldPos = tilemap.CellToWorld(pos) + tilemap.tileAnchor;
            GameObject prefabInstance = Instantiate(tilePrefab, worldPos, Quaternion.identity);
            tilePrefabs[pos] = prefabInstance;
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        Vector2 hitPosition = other.contacts[0].point;
        Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

       
        if (!tilemap.HasTile(cellPosition) || !tileHealth.ContainsKey(cellPosition))
            return;

       
        tileHealth[cellPosition]--;
        Debug.Log($"Tile hit at {cellPosition}, health remaining: {tileHealth[cellPosition]}");

        
        if (tileHealth[cellPosition] == 1 && tilePrefabs.ContainsKey(cellPosition))
        {
            TileBehaviour tileBehaviour = tilePrefabs[cellPosition].GetComponent<TileBehaviour>();
            if (tileBehaviour != null)
            {
                tileBehaviour.isDamaged();
            }
        }

        // Break tile if health is 0
        if (tileHealth[cellPosition] <= 0)
        {
            BreakTile(cellPosition);
            GameManager.Instance.AddTileScore();
        }
    }

    private void BreakTile(Vector3Int cellPosition)
    {
        tilemap.SetTile(cellPosition, null);

        if (tilePrefabs.ContainsKey(cellPosition))
        {
            Destroy(tilePrefabs[cellPosition]);
            tilePrefabs.Remove(cellPosition);
        }

        tileHealth.Remove(cellPosition);
    }
}