using UnityEngine;
using UnityEngine. SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverUI;
    public GameObject victoryUI;

    [Header("Enemy Tracking")]
    public int totalEnemiesToKill = 5;
    private int currentKills = 0;

    private bool gameHasEnded = false;

    public void Awake()
    {
        if (instance == null) instance = this;
    }

    public void RegisterKill()
    {
        currentKills++;
        Debug.Log("Enemies Defeated: " + currentKills + " / " + totalEnemiesToKill);

        if (currentKills >= totalEnemiesToKill && !gameHasEnded)
        {
            WinGame();
        }
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("GAME OVER");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void WinGame ()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;

            victoryUI.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("VICTORY!");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartLevel ()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
}
