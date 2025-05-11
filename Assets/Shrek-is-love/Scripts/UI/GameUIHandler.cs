using UnityEngine;

public class GameUIHandler
{
    private GameObject deathScreen;
    private GameObject pauseMenu;
    private UIManager uiManager;
    private bool isPaused;
    
    public GameUIHandler(GameObject death, GameObject pause, UIManager manager)
    {
        deathScreen = death;
        pauseMenu = pause;
        uiManager = manager;
    }
    
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!isPaused && !deathScreen.activeSelf)
            {
                PauseGame();
            }
            else if(isPaused && !deathScreen.activeSelf)
            {
                ResumeGame();
            }
        }
    }
    
    public void DeathSequence()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void PauseGame()
    {
        isPaused = true;
        uiManager.ShowScreen<PauseMenuScreen>();
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        uiManager.GoBack();
        Time.timeScale = 1f;
    }
}