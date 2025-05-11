using UnityEngine.UI;

public class ButtonSoundController
{
    private AudioManager audioManager;
    
    public ButtonSoundController(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }
    
    public void SetupButton(Button button)
    {
        button.onClick.AddListener(() => audioManager.Play("Click"));
    }
}