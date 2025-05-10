using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class InputManager
{
    private KeyboardState prevKeyboard;
    private KeyboardState keyboard;

    private GamePadState prevGamepad;
    private GamePadState gamepad;

    public Vector2 MoveInput => GetMoveInput();

    public Vector2 GetMoveInput()
    {
        if (gamepad.IsConnected)
        {
            return gamepad.ThumbSticks.Left;
        }
        else
        {
            var move = new Vector2(0);
            if (keyboard.IsKeyDown(Keys.A))
            {
                move.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                move.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                move.Y -= 1;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                move.Y += 1;
            }
            if (move.LengthSquared() != 0)
            {
                move.Normalize();
            }
            return move;
        }
    }

    public void Update()
    {
        prevKeyboard = keyboard;
        prevGamepad = gamepad;

        keyboard = Keyboard.GetState();
        gamepad = GamePad.GetState(PlayerIndex.One);
    }

    public bool KeyJustPressed(Keys key)
    {
        return prevKeyboard.IsKeyUp(key) && keyboard.IsKeyDown(key);
    }

    public bool KeyJustReleased(Keys key)
    {
        return prevKeyboard.IsKeyDown(key) && keyboard.IsKeyUp(key);
    }
}
