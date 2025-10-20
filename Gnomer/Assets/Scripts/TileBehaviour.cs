using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBehaviour : MonoBehaviour
{


    [SerializeField] public static int Health;

    private Rigidbody2D _rb;



    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _rb = GetComponent<Rigidbody2D>();

        Health = 2;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
