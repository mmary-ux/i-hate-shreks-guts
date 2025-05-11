using UnityEngine.UI;

public class PeacefulModeTogglesManager
{
    private Toggle toggle;
    private GameSettingsManager settingsManager;
    
    public PeacefulModeTogglesManager(Toggle toggle, GameSettingsManager settingsManager)
    {
        this.toggle = toggle;
        this.settingsManager = settingsManager;
        
        toggle.isOn = settingsManager.PeacefulModeEnabled;
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }
    
    public void OnToggleChanged(bool isOn)
    {
        settingsManager.SetPeacefulMode(isOn);
    }
    
    public void Show()
    {
        toggle.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        toggle.gameObject.SetActive(false);
    }
}