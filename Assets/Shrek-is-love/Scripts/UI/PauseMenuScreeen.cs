using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScreen : UIScreen
{
    private Button resumeButton;
    private Button saveButton;
    private Button loadButton;
    private Button mainMenuButton;
    private DataPersistenceManager dataManager;
    
    public PauseMenuScreen(Button resume, Button save, Button load, Button mainMenu, 
                          DataPersistenceManager dataManager)
    {
        this.resumeButton = resume;
        this.saveButton = save;
        this.loadButton = load;
        this.mainMenuButton = mainMenu;
        this.dataManager = dataManager;
        
        resumeButton.onClick.AddListener(OnResumeClicked);
        saveButton.onClick.AddListener(OnSaveClicked);
        loadButton.onClick.AddListener(OnLoadClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }
    
    private void OnResumeClicked()
    {
        Manager.GoBack();
    }
    
    private void OnSaveClicked()
    {
        dataManager.SaveGame();
    }
    
    private void OnLoadClicked()
    {
        dataManager.LoadGame();
    }
    
    private void OnMainMenuClicked()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public override void Show()
    {
        resumeButton.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        loadButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public override void Hide()
    {
        resumeButton.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}