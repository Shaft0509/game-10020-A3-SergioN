using UnityEngine;

public class DronePatrolState : State
{
    private DroneStateMachine stateMachine;
    private DroneAI drone;

    public DronePatrolState(DroneStateMachine sm, DroneAI d)
    {
        stateMachine = sm;
        drone = d;
    }

    public override void Enter()
    {
        // Patrol begins
    }

    public override void Execute()
    {
        // Move to current waypoint
        Transform point = drone.patrolPoints[drone.CurrentWaypoint];
        drone.Agent.SetDestination(point.position);

        // Go to next point
        if (Vector3.Distance(drone.transform.position, point.position) < 1f)
        {
            drone.CurrentWaypoint =
                (drone.CurrentWaypoint + 1) % drone.patrolPoints.Length;
        }

        // If player seen, start chase
        if (drone.CanSeePlayer())
        {
            stateMachine.ChangeState(new DroneChaseState(stateMachine, drone));
        }
    }

    public override void Exit()
    {
        // Leaving patrol
    }
}