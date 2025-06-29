using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TeamCherry.Project;

class InputManager
{
    private KeyboardState prevKeyboard;
    private KeyboardState keyboard;

    private GamePadState prevGamepad;
    private GamePadState gamepad;

    public bool IsGamepadConnected => gamepad.IsConnected;
    public Vector2 MoveInput { get; protected set; }

    private void UpdateMovementInput()
    {
        var moveInput = Vector2.Zero;
        if (gamepad.IsConnected)
        {
            moveInput = gamepad.ThumbSticks.Left;
            moveInput.Y = -moveInput.Y;
        }
        else
        {
            if (keyboard.IsKeyDown(Keys.A))
            {
                moveInput.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                moveInput.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                moveInput.Y -= 1;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                moveInput.Y += 1;
            }
            if (moveInput.LengthSquared() > 0)
            {
                moveInput.Normalize();
            }
        }
        MoveInput = moveInput;
    }

    public void Update()
    {
        prevKeyboard = keyboard;
        prevGamepad = gamepad;

        keyboard = Keyboard.GetState();
        gamepad = GamePad.GetState(PlayerIndex.One);

        UpdateMovementInput();
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
