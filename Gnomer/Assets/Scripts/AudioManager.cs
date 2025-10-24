using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{

    private Scene GameScene;
   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (GameManager.Instance.CurrentState == Utilities.GameState.MainMenu)
        {
            DontDestroyOnLoad(gameObject);
        }
        
        

        

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.CurrentState == Utilities.GameState.Victory)
        {
            Destroy(gameObject);
        }
        
    }
}
