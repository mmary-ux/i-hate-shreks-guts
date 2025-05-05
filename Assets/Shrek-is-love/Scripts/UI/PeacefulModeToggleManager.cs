using UnityEngine;
using UnityEngine.UI;

public class PeacefulModeToggleManager : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    
    private void Start()
    {
        toggle.isOn = GameSettingsManager.Instance.PeacefulModeEnabled;
        
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }
    
    public void OnToggleChanged(bool isOn)
    {
        GameSettingsManager.Instance.SetPeacefulMode(isOn);
    }
    
    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}