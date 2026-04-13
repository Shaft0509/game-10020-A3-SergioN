using UnityEngine;
using UnityEngine.AI;

public class RepairBotAI : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform homePoint;
    public Transform repairPoint;

    // Public for state scripts
    public NavMeshAgent Agent { get; private set; }

    void Awake()
    {
        // Get NavMeshAgent
        Agent = GetComponent<NavMeshAgent>();
    }
}