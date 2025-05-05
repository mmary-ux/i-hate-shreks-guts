using UnityEngine;

public class BossVision : MonoBehaviour
{
    public BossSettings settings;

    public bool IsPlayerVisible(out Vector3 playerPosition)
    {
        playerPosition = Vector3.zero;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > settings.detectionRadius)
        {
            return false;
        }

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, settings.sphereCastRadius,
            directionToPlayer, out hit, settings.detectionRadius, settings.obstacleMask))
        {
            if (hit.distance < distanceToPlayer)
            {
                return false;
            }
        }

        playerPosition = player.transform.position;
        return true;
    }
}