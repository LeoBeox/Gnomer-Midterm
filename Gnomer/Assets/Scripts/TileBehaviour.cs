using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBehaviour : MonoBehaviour
{

    private SpriteRenderer _sprRenderer;
    [SerializeField] public Sprite spr_damaged;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _sprRenderer = GetComponent<SpriteRenderer>();

        _sprRenderer.sortingOrder = 1; // Above Tilemap

    }

    public void isDamaged()
    {
        _sprRenderer.sprite = spr_damaged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
