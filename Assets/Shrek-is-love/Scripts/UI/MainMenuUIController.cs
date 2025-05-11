using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuUIController : BaseSceneUIController
{
    [Header("Main Menu")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject mainButtons;
    
    [Header("Settings Menu")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button backButton;
    [SerializeField] private Toggle peacefulToggle;
    [SerializeField] private GameObject settingsMenu;
    
    private void Start()
    {
        var mainMenu = new MainMenuScreen(playButton, loadButton, settingsButton, quitButton, 
                                        mainButtons, DataPersistenceManager.instance);
        
        var settings = new SettingsScreen(audioMixer, musicSlider, sfxSlider, backButton, 
                                        peacefulToggle, settingsMenu, GameSettingsManager.Instance);
        
        uiManager.RegisterScreen(mainMenu);
        uiManager.RegisterScreen(settings);
        
        SetupButton(playButton);
        SetupButton(loadButton);
        SetupButton(settingsButton);
        SetupButton(quitButton);
        SetupButton(backButton);
        
        uiManager.ShowScreen<MainMenuScreen>();
    }
}