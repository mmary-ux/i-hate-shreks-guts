using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScreen : UIScreen
{
    private AudioMixer audioMixer;
    private Slider musicSlider;
    private Slider sfxSlider;
    private Button backButton;
    private GameObject settingsMenu;
    private PeacefulModeToggleManager peacefulModeToggle;
    
    public SettingsScreen(AudioMixer mixer, Slider music, Slider sfx, Button back, 
                         Toggle peacefulToggle, GameObject settingsMenu, GameSettingsManager settingsManager)
    {
        audioMixer = mixer;
        musicSlider = music;
        sfxSlider = sfx;
        backButton = back;
        this.settingsMenu = settingsMenu;
        
        peacefulModeToggle = new PeacefulModeToggleManager(peacefulToggle, settingsManager);
        
        backButton.onClick.AddListener(OnBackPressed);
        musicSlider.onValueChanged.AddListener(SetVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        
        if(PlayerPrefs.HasKey("musicVolume")) LoadVolume();
        if(PlayerPrefs.HasKey("sfxVolume")) LoadSFXVolume();
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxvolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetVolume(musicSlider.value);
    }
    
    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume(sfxSlider.value);
    }
    
    public override void Show()
    {
        settingsMenu.SetActive(true);
    }
    
    public override void Hide()
    {
        settingsMenu.SetActive(false);
    }
}