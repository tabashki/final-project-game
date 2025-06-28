
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TeamCherry.Project;

public class GameStateManager : IGameStateManager
{
    private const int MaxStates = 8;
    
    private int firstToDraw = 0;
    private List<IGameState> stateStack = new();
    private ContentManager contentManager;

    // Enumerable of the state stack, ordered top-to-bottom
    public IEnumerable<IGameState> States => stateStack.AsEnumerable().Reverse();

    public GameStateManager(ContentManager content)
    {
        contentManager = content;
    }

    public void PushState(IGameState state)
    {
        if (stateStack.Count < MaxStates)
        {
            stateStack.Add(state);
            state.Load(this, contentManager);
        }
        else
        {
            throw new Exception($"Cannot contain more than {MaxStates} game states");
        }
    }

    public void PopState()
    {
        if (stateStack.Count > 0)
        {
            var state = stateStack.Last();
            stateStack.RemoveAt(stateStack.Count - 1);
            state.Unload();
        }
        else
        {
            throw new Exception("No game state to pop");
        }
    }

    public void Update(GameTime gameTime)
    {
        bool continueUpdate = true;
        bool continueDraw = true;
            
        for (int i = stateStack.Count - 1; i >= 0; i--)
        {
            var state = stateStack[i];
            if (continueUpdate)
            {
                state.Update(gameTime);
                continueUpdate = state.PropagateUpdate;
            }
            if (continueDraw)
            {
                firstToDraw = i;
                continueDraw = state.PropagateDraw;
            }
        }
    }

    public void Draw(GameTime gameTime)
    {
        for (int i = firstToDraw; i < stateStack.Count; i++)
        {
            stateStack[i].Draw(gameTime);
        }
    }
}
