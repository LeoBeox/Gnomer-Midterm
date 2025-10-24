using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using JetBrains.Annotations;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEngine.Audio;

public class PlayerBehaviour : MonoBehaviour
{

    public float Speed = 7f;
    private float _directionX, _directionY;
    

    [SerializeField] public KeyCode _rightDirection;
    [SerializeField] public KeyCode _leftDirection;
    [SerializeField] public KeyCode _upDirection;
    [SerializeField] public KeyCode _downDirection;

    [SerializeField] private AudioResource _treasureGet;

    [SerializeField] private AudioResource _powerUp;

    private AudioSource _source;

    

    private Rigidbody2D _rb;

    // Power Up Variables

    private float _currentSpeedMultiplier = 1f;
    private bool _hasSpeedBoost = false;
   
   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        _rb = GetComponent<Rigidbody2D>();

        _source = GetComponent<AudioSource>();
    

    }

    // Updates linear velocity with direction and speed
    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState == Utilities.GameState.Playing)
        {
            _rb.linearVelocity = new Vector2(_directionX * Speed, _directionY * Speed);
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.CurrentState != Utilities.GameState.Playing)
        {
            _directionX = 0.0f;
            _directionY = 0.0f;
            return;
        }

        _directionX = 0.0f;
        _directionY = 0.0f;

        // Keys for directions
        if (Input.GetKey(_rightDirection))
            _directionX += 1.0f;
        if (Input.GetKey(_leftDirection))
            _directionX -= 1.0f;
        if (Input.GetKey(_upDirection))
            _directionY += 1.0f;
        if (Input.GetKey(_downDirection))
            _directionY -= 1.0f;


        // Debug.Log("Going down " + _directionY);

        if (_hasSpeedBoost)
        {
            _directionX *= _currentSpeedMultiplier;
            _directionY *= _currentSpeedMultiplier;
        }
    }


    // When entering collisions, stop movements
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Tile"))
        {
            // _directionY = 0.0f;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Treasure"))
        {
            _source.resource = _treasureGet;
        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            _source.resource = _powerUp;
        }

        _source.Play();
    }

    public void TakeDamage()
    {
        GameManager.Instance.Health -= 1;
    }

    public void ActivateSpeedBoost(float multiplier, float duration)
    {
        // If already has speed boost, restart the timer
        if (_hasSpeedBoost)
        {
            StopCoroutine(nameof(SpeedBoostCoroutine));
        }

        StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }
    
    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        _hasSpeedBoost = true;
        _currentSpeedMultiplier = multiplier;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = Color.white;
        if (sr != null)
        {
            originalColor = sr.color;
            sr.color = Color.yellow;
        }

        yield return new WaitForSeconds(duration);

        _currentSpeedMultiplier = 1.0f;
        _hasSpeedBoost = false;

        if (sr != null)
        {
            sr.color = originalColor;
        }
    }
}
