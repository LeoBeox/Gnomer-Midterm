using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryPaneManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI playAgainText;
    public TextMeshProUGUI quitText;

    void Start()
    {

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Check if game is in Victory state
        if (GameManager.Instance.CurrentState == Utilities.GameState.Victory)
        {
            // Show victory screen if not already shown
            if (victoryPanel != null && !victoryPanel.activeSelf)
            {
                ShowVictoryScreen();
            }

            // Handle input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayAgain();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitToMainMenu();
            }
        }
    }

    private void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);

        // Update text
        if (titleText != null)
        {
            titleText.text = "VICTORY!";
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = $"Final Score: {GameManager.Instance.Score}";
        }

        if (playAgainText != null)
        {
            playAgainText.text = "Press SPACE to Play Again";
        }

        if (quitText != null)
        {
            quitText.text = "Press ESC to Quit";
        }

        Debug.Log("Victory screen shown!");
    }

    private void PlayAgain()
    {
        
        victoryPanel.SetActive(false);

        // Reload the current scene
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    private void QuitToMainMenu()
    {
        victoryPanel.SetActive(false);
        
        // Load main menu scene
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene("MainMenu");
        
    }
}