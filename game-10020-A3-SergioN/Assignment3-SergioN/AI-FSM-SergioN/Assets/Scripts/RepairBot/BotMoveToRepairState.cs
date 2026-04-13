using UnityEngine;

public class BotMoveToRepairState : State
{
    private BotStateMachine stateMachine;
    private RepairBotAI bot;

    public BotMoveToRepairState(BotStateMachine sm, RepairBotAI b)
    {
        stateMachine = sm;
        bot = b;
    }

    public override void Enter()
    {
        // Move to drone repair point
        bot.Agent.SetDestination(bot.repairPoint.position);
    }

    public override void Execute()
    {
        // Once reached repair point -> repair
        if (Vector3.Distance(bot.transform.position, bot.repairPoint.position) < 1f)
        {
            stateMachine.ChangeState(new BotRepairState(stateMachine, bot));
        }
    }

    public override void Exit()
    {
    }
}