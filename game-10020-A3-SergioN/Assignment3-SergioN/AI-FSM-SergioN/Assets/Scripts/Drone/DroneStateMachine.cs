using UnityEngine;

public class DroneStateMachine : MonoBehaviour
{
    public DroneAI drone;
    private State currentState;

    void Start()
    {
        // Start in patrol state
        ChangeState(new DronePatrolState(this, drone));
    }

    void Update()
    {
        // Run active state
        currentState?.Execute();
    }

    public void ChangeState(State newState)
    {
        // Exit old state
        currentState?.Exit();

        // Switch state
        currentState = newState;

        // Enter new state
        currentState.Enter();
    }
}