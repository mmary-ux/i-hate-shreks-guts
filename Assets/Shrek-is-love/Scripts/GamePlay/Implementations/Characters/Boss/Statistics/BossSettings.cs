using UnityEngine;

[CreateAssetMenu(fileName = "BossSettings", menuName = "AI/Boss Settings")]
public class BossSettings : ScriptableObject
{
    [Header("Vision")]
    public float detectionRadius = 20f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float sphereCastRadius = 1f;

    [Header("Rotation")]
    public float rotationSpeed = 5f;

    [Header("Combat")]
    public float timeBetweenAttacks = 3f;
    public int manaForSpecialAttack = 100;

    [Header("Containment Field")]
    [SerializeField] public GameObject forceFieldPrefab;
    public float timeBetweenForcetFields = 10f;
    public float forceFieldDuration = 5f;
    [SerializeField] public GameObject minionPrefab;
    public int minionsToSpawn = 3;
    public float spawnRadius = 5f;
}