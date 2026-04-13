using UnityEngine;

public class BotIdleState : State
{
    private BotStateMachine stateMachine;
    private RepairBotAI bot;

    public BotIdleState(BotStateMachine sm, RepairBotAI b)
    {
        stateMachine = sm;
        bot = b;
    }

    public override void Enter()
    {
        // Stay at home
        bot.Agent.SetDestination(bot.homePoint.position);
    }

    public override void Execute()
    {
        // Waiting for repair call
    }

    public override void Exit()
    {
    }
}