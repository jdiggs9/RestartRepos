using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Buttons : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;

    public void MenuPause() 
    {
        pauseMenu.SetActive(true);
    }
    public void MenuHome()
    {
        SceneManager.LoadScene("Title");
    }
    public void MenuResume()
    {
        pauseMenu.SetActive(false);
    }
    public void MenuRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
    }
    public void MenuSettings()
    {

    }
    public void MenuQuit()
    {
        EditorApplication.isPlaying = false;
    }
    public void MenuStart()
    {
        SceneManager.LoadScene("Level1");
    }
}
