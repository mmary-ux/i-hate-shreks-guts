using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance { get; private set; }
    
    public bool PeacefulModeEnabled { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        PeacefulModeEnabled = PlayerPrefs.GetInt("PeacefulMode", 0) == 1;
    }
    
    public void SetPeacefulMode(bool enabled)
    {
        PeacefulModeEnabled = enabled;
        PlayerPrefs.SetInt("PeacefulMode", enabled ? 1 : 0);
        
        UpdateAllEnemies();
    }
    
    private void UpdateAllEnemies()
    {
        EnemyAI[] allEnemies = FindObjectsOfType<EnemyAI>(true); // true включает неактивные объекты
        foreach (EnemyAI enemy in allEnemies)
        {
            if (enemy != null)
            {
                enemy.SetPeacefulMode(PeacefulModeEnabled);
            }
        }
        
        Debug.Log($"Peaceful mode set to {PeacefulModeEnabled} for {allEnemies.Length} enemies");
    }
    
    public void OnSceneLoaded()
    {
        UpdateAllEnemies();
    }
}