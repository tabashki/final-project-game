
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TeamCherry.Project;

public class PlayingGameState : IGameState
{
    public bool PropagateUpdate { get; } = false;
    public bool PropagateDraw { get; } = true;
    
    public void Load(IGameStateManager stateManager, ContentManager contentManager)
    {
        throw new NotImplementedException();
    }

    public void Unload()
    {
        throw new NotImplementedException();
    }

    public void Update(GameTime gameTime)
    {
        throw new NotImplementedException();
    }

    public void Draw(GameTime gameTime)
    {
        throw new NotImplementedException();
    }
}
