
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TeamCherry.Project;

public interface IGameState
{
    // If true signals to GameStateManager to update the next state down the stack
    public bool PropagateUpdate { get; }
    
    // If true signals to GameStateManager to draw the next state down the stack
    // NOTE: States are drawn back to front, to allow overlaying by upper states
    public bool PropagateDraw { get; }

    public void Load(IGameStateManager stateManager, ContentManager contentManager);
    public void Unload();
    
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
}
