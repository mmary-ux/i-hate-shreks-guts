using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    private Button playButton;
    private Button loadButton;
    private Button settingsButton;
    private Button quitButton;
    private DataPersistenceManager dataManager;
    
    public MainMenuScreen(Button play, Button load, Button settings, Button quit, 
                         DataPersistenceManager dataManager)
    {
        this.playButton = play;
        this.loadButton = load;
        this.settingsButton = settings;
        this.quitButton = quit;
        this.dataManager = dataManager;
        
        playButton.onClick.AddListener(OnPlayClicked);
        loadButton.onClick.AddListener(OnLoadClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        
        if(!dataManager.HasGameData())
        {
            loadButton.interactable = false;
        }
    }
    
    private void OnPlayClicked() 
    {
        dataManager.NewGame();
        SceneManager.LoadSceneAsync("Level1");
    }
    
    private void OnLoadClicked()
    {
        dataManager.LoadGame();
        SceneManager.LoadSceneAsync("Level1");
    }
    
    private void OnSettingsClicked()
    {
        Manager.ShowScreen<SettingsScreen>();
    }
    
    private void OnQuitClicked()
    {
        Application.Quit();
    }
    
    public override void Show()
    {
        playButton.gameObject.SetActive(true);
        loadButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public override void Hide()
    {
        playButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
}