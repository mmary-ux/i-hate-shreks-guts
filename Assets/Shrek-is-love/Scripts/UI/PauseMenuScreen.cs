using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScreen : UIScreen
{
    private Button saveButton;
    private Button loadButton;
    private Button mainMenuButton;
    private GameObject pauseMenu;
    private DataPersistenceManager dataManager;
    
    public PauseMenuScreen(Button save, Button load, Button mainMenu, 
                         GameObject pauseMenu, DataPersistenceManager dataManager)
    {
        this.saveButton = save;
        this.loadButton = load;
        this.mainMenuButton = mainMenu;
        this.dataManager = dataManager;
        this.pauseMenu = pauseMenu;
        
        saveButton.onClick.AddListener(OnSaveClicked);
        loadButton.onClick.AddListener(OnLoadClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }
    
    private void OnSaveClicked()
    {
        dataManager.SaveGame();
    }
    
    private void OnLoadClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        dataManager.LoadGame();
    }
    
    private void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public override void Show()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public override void Hide()
    {
        pauseMenu.SetActive(false);
    }
}