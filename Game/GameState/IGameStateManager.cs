namespace TeamCherry.Project;

public interface IGameStateManager
{
    public void PushState(IGameState state);

    public void PopState();
}