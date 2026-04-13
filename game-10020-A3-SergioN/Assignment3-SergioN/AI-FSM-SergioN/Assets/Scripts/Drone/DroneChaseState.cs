using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneChaseState : State
{
    private DroneStateMachine stateMachine;
    private DroneAI drone;

    public DroneChaseState(DroneStateMachine sm, DroneAI d)
    {
        stateMachine = sm;
        drone = d;
    }

    public override void Enter()
    {
        // Chase starts
    }

    public override void Execute()
    {
        // Follow player
        drone.Agent.SetDestination(drone.player.position);

        if (drone.CanSeePlayer())
        {
            // Save last player position
            drone.lastSeenPosition = drone.player.position;
        }
        else
        {
            // Lost player -> search
            stateMachine.ChangeState(new DroneSearchState(stateMachine, drone));
        }

        // If close enough -> END scene
        float dist = Vector3.Distance(drone.transform.position, drone.player.position);

        if (dist < drone.catchDistance)
        {
            SceneManager.LoadScene(drone.gameOverScene);
        }
    }

    public override void Exit()
    {
        // Leaving chase
    }
}