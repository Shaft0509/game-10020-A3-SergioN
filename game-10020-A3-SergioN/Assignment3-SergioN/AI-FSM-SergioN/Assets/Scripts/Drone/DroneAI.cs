using UnityEngine;
using UnityEngine.AI;

public class DroneAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    public string gameOverScene = "END";

    [Header("Vision")]
    public float sightDistance = 8f;
    public float sightAngle = 60f;
    public float catchDistance = 1.5f;

    [Header("Search")]
    public float searchTime = 4f;

    // NavMeshAgent used by state scripts
    public NavMeshAgent Agent { get; private set; }

    // Current patrol point index
    public int CurrentWaypoint { get; set; }

    // Last known player location
    public Vector3 lastSeenPosition;

    // Signal used by repair bot
    public bool needsRepair = false;

    void Awake()
    {
        // Get NavMeshAgent
        Agent = GetComponent<NavMeshAgent>();
        CurrentWaypoint = 0;
    }

    public bool CanSeePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        // Distance check
        if (distance > sightDistance) return false;

        // Vision cone angle check
        float angle = Vector3.Angle(transform.forward, toPlayer.normalized);
        if (angle > sightAngle * 0.5f) return false;

        // Raycast line of sight
        if (Physics.Raycast(transform.position, toPlayer.normalized, out RaycastHit hit, sightDistance))
        {
            return hit.transform == player;
        }

        return false;
    }
}