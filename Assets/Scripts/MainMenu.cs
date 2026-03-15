using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelName = "Level_01";

    public void StartGame()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(firstLevelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
