using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameInput gameInput; // Reference to the GameInput script
    private bool isPaused = false; // Tracks pause state

    private void Update()
    {
        // Check if the pause button was pressed
        if (gameInput != null && gameInput.IsPausePressed())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pauses the game and loads the pause scene
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Freeze game time
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive); // Load the pause scene
    }

    // Resumes the game and unloads the pause scene
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Unfreeze game time
        SceneManager.UnloadSceneAsync("Pause"); // Unload the pause scene
    }
}
