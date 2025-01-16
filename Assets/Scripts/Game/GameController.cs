using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject headsUpDisplay;
    public GameObject deathMenu;
    public GameObject storeMenu;
    public bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !storeMenu.activeInHierarchy)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !storeMenu.activeInHierarchy)
        {
            ResumeGame();
        }

        Debug.Log(Time.timeScale.ToString());
    }

    // Pauses the game
    public void PauseGame()
    {
        PauseTime();
        pauseMenu.SetActive(true);
        headsUpDisplay.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    // Resumes the game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        headsUpDisplay.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;

        Debug.Log("Resumed");
    }

    // Shows the death screen upon player death
    public void PlayerDead()
    {
        PauseTime();
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    // Restarts the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
