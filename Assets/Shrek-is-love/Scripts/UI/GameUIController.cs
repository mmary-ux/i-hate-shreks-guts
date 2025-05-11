using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button backButton;
    [SerializeField] private Toggle peacefulToggle;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button pauseLoadButton;
    [SerializeField] private Button pauseMainMenuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button deathMainMenuButton;
    [SerializeField] private GameObject deathScreenObject;
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private AudioMixer audioMixer;
    
    private UIManager uiManager;
    private GameUIHandler gameUIHandler;
    private ButtonSoundController soundController;
    
    private void Start()
    {
        // Инициализация менеджера UI
        uiManager = new UIManager();
        
        // Инициализация звуков кнопок
        soundController = new ButtonSoundController(FindObjectOfType<AudioManager>());
        
        // Создание всех экранов
        var mainMenu = new MainMenuScreen(playButton, loadButton, settingsButton, quitButton, 
                                        DataPersistenceManager.instance);
        var settings = new SettingsScreen(audioMixer, musicSlider, sfxSlider, backButton, 
                                        peacefulToggle, GameSettingsManager.Instance);
        var pauseMenu = new PauseMenuScreen(resumeButton, saveButton, pauseLoadButton, 
                                          pauseMainMenuButton, DataPersistenceManager.instance);
        var deathScreen = new DeathScreen(restartButton, deathMainMenuButton, 
                                        DataPersistenceManager.instance);
        
        // Регистрация экранов
        uiManager.RegisterScreen(mainMenu);
        uiManager.RegisterScreen(settings);
        uiManager.RegisterScreen(pauseMenu);
        uiManager.RegisterScreen(deathScreen);
        
        // Настройка звуков кнопок
        soundController.SetupButton(playButton);
        soundController.SetupButton(loadButton);
        soundController.SetupButton(settingsButton);
        soundController.SetupButton(quitButton);
        soundController.SetupButton(backButton);
        soundController.SetupButton(saveButton);
        soundController.SetupButton(pauseLoadButton);
        soundController.SetupButton(pauseMainMenuButton);
        soundController.SetupButton(restartButton);
        soundController.SetupButton(deathMainMenuButton);
        
        // Инициализация игрового UI обработчика
        gameUIHandler = new GameUIHandler(deathScreenObject, pauseMenuObject, uiManager);
        
        // Показать главное меню
        uiManager.ShowScreen<MainMenuScreen>();
    }
    
    private void Update()
    {
        gameUIHandler.Update();
    }
}