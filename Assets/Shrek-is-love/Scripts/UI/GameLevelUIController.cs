using UnityEngine;
using UnityEngine.UI;

public class GameLevelUIController : BaseSceneUIController
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button mainMenuButton;
    
    [Header("Death Screen")]
    [SerializeField] private GameObject deathScreenObject;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button deathMainMenuButton;
    
    private GameUIHandler gameUIHandler;
    
    private void Start()
    {
        var pauseMenu = new PauseMenuScreen(saveButton, loadButton, 
                                          mainMenuButton, pauseMenuObject, DataPersistenceManager.instance);
        
        var deathScreen = new DeathScreen(restartButton, deathMainMenuButton, deathScreenObject,
                                          DataPersistenceManager.instance);
        
        uiManager.RegisterScreen(pauseMenu);
        uiManager.RegisterScreen(deathScreen);
        
        SetupButton(saveButton);
        SetupButton(loadButton);
        SetupButton(mainMenuButton);
        SetupButton(restartButton);
        SetupButton(deathMainMenuButton);
        
        gameUIHandler = new GameUIHandler(deathScreenObject, pauseMenuObject, uiManager);
    }
    
    private void Update()
    {
        gameUIHandler?.Update();
    }
    
    public void ShowDeathScreen()
    {
        gameUIHandler?.DeathSequence();
    }
}