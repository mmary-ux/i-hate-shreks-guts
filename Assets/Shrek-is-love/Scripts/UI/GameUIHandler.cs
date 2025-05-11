using UnityEngine;

public class GameUIHandler
{
    private GameObject deathScreen;
    private GameObject pauseMenu;
    private UIManager uiManager;
    private bool isPaused;
    
    public GameUIHandler(GameObject deathScreen, GameObject pauseMenu, UIManager manager)
    {
        this.deathScreen = deathScreen;
        this.pauseMenu = pauseMenu;
        this.uiManager = manager;
        
        deathScreen.SetActive(false);
        pauseMenu.SetActive(false);
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
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}