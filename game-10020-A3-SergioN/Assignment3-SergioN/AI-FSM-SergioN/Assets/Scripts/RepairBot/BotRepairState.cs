using UnityEngine;

public class BotRepairState : State
{
    private BotStateMachine stateMachine;
    private RepairBotAI bot;
    private float repairStart;

    public BotRepairState(BotStateMachine sm, RepairBotAI b)
    {
        stateMachine = sm;
        bot = b;
    }

    public override void Enter()
    {
        // Start repair timer
        repairStart = Time.time;
    }

    public override void Execute()
    {
        // After repair time, reset signal and go home
        if (Time.time - repairStart > 3f)
        {
            stateMachine.drone.needsRepair = false;
            stateMachine.ChangeState(new BotIdleState(stateMachine, bot));
        }
    }

    public override void Exit()
    {
    }
}