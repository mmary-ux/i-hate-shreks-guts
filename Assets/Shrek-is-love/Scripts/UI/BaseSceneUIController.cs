using UnityEngine.UI;
using UnityEngine;

public abstract class BaseSceneUIController : MonoBehaviour
{
    protected UIManager uiManager;
    protected ButtonSoundController soundController;
    
    protected virtual void Awake()
    {
        uiManager = new UIManager();
        soundController = new ButtonSoundController(FindObjectOfType<AudioManager>());
    }
    
    protected void SetupButton(Button button)
    {
        soundController.SetupButton(button);
    }
}