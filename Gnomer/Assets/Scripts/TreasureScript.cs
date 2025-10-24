using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TreasureScript : MonoBehaviour
{

    public int scoreValue = 100;

    private SpriteRenderer sprRenderer;


    private static int treasuresCollected = 0;
    private static int totalTreasures = 0;
    private bool hasInited = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        sprRenderer.sortingOrder = -1;

        StartCoroutine(Rotate());

        if (!hasInited)
        {
            treasuresCollected = 0;
            totalTreasures = FindObjectsByType<TreasureScript>(FindObjectsSortMode.None).Length;
            hasInited = true;
            Debug.Log($"Total treasures in level: {totalTreasures}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectTreasure();
        }

        
    }

    private void CollectTreasure()
    {
        GameManager.Instance.AddTreasureScore();
        treasuresCollected++;
        Debug.Log("Collected a treasure!");

        if (treasuresCollected == totalTreasures)
        {
            Debug.Log("All treasures collected");
            hasInited = false;
            GameManager.Instance.Victory();
        }

        Destroy(gameObject);
    }
        
    void OnDestroy()
    {
        // If this is the last treasure being destroyed and scene is changing, reset
        if (FindObjectsByType<TreasureScript>(FindObjectsSortMode.None).Length <= 1)
        {
            hasInited = false;
        }
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(0, 0, 100 * Time.deltaTime);
            yield return null;
        }
    }
}
