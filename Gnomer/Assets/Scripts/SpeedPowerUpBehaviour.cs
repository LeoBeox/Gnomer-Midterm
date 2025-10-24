using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Speed : MonoBehaviour
{

    // Power Up Settings
    public float speedMultiplier;
    public float duration;

    private SpriteRenderer sprRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        sprRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(RotatePowerUp());
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("enterd trigger for collision!");

        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                Debug.Log("enterd if statement for powerup!");
                player.ActivateSpeedBoost(speedMultiplier, duration);
                GameManager.Instance.AddScore(50);

                Disappear();
            }

        }


    }

    void Disappear()
    {
        Destroy(gameObject);
    }

    private IEnumerator RotatePowerUp()
    {
        while (true)
        {
            transform.Rotate(0, 0, 100 * Time.deltaTime);
            yield return null;
        }
    }
}
