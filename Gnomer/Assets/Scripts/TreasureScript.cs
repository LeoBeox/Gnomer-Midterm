using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class TreasureScript : MonoBehaviour
{

    public int scoreValue = 100;

    private SpriteRenderer sprRenderer;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        sprRenderer.sortingOrder = -1;

        StartCoroutine(Rotate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddTreasureScore();
            Destroy(gameObject);
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
