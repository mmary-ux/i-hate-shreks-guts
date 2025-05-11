using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : UIScreen
{
    private Button restartButton;
    private Button mainMenuButton;
    private DataPersistenceManager dataManager;
    
    public DeathScreen(Button restart, Button mainMenu, DataPersistenceManager dataManager)
    {
        this.restartButton = restart;
        this.mainMenuButton = mainMenu;
        this.dataManager = dataManager;
        
        restartButton.onClick.AddListener(OnRestartClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }
    
    private void OnRestartClicked()
    {
        dataManager.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void OnMainMenuClicked()
    {
        dataManager.ResetGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public override void Show()
    {
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public override void Hide()
    {
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }
}