public abstract class State
{
    // Called once when entering the state
    public abstract void Enter();

    // Called every frame while active
    public abstract void Execute();

    // Called once when leaving the state
    public abstract void Exit();
}