using UnityEngine;

public class DroneSearchState : State
{
    private DroneStateMachine stateMachine;
    private DroneAI drone;
    private float startTime;

    public DroneSearchState(DroneStateMachine sm, DroneAI d)
    {
        stateMachine = sm;
        drone = d;
    }

    public override void Enter()
    {
        // Start search timer
        startTime = Time.time;

        // Move to last seen location
        drone.Agent.SetDestination(drone.lastSeenPosition);
    }

    public override void Execute()
    {
        // If player is found again
        if (drone.CanSeePlayer())
        {
            stateMachine.ChangeState(new DroneChaseState(stateMachine, drone));
            return;
        }

        // If search ends, request repair bot
        if (Time.time - startTime > drone.searchTime)
        {
            drone.needsRepair = true;
            stateMachine.ChangeState(new DronePatrolState(stateMachine, drone));
        }
    }

    public override void Exit()
    {
    }
}