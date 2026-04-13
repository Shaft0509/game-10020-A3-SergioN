using UnityEngine;

public class BotStateMachine : MonoBehaviour
{
    public RepairBotAI bot;
    public DroneAI drone;

    private State currentState;

    void Start()
    {
        // Start bot at home
        ChangeState(new BotIdleState(this, bot));
    }

    void Update()
    {
        currentState?.Execute();

        // If drone requests repair, move to it
        if (drone.needsRepair &&
            !(currentState is BotMoveToRepairState) &&
            !(currentState is BotRepairState))
        {
            ChangeState(new BotMoveToRepairState(this, bot));
        }
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}