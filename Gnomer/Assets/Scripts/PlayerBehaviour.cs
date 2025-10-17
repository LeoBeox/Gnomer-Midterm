using UnityEngine;
using Unity.VisualScripting;

public class PlayerBehaviour : MonoBehaviour
{

    public float Speed = 7f;
    private float _direction;

    [SerializeField] public KeyCode _rightDirection;
    [SerializeField] public KeyCode _leftDirection;
    [SerializeField] public KeyCode _downDirection;

    private Rigidbody2D _rb;
   
   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();

    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
