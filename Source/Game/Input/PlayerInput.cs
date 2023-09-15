using System;

namespace Demo.Source.Game;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerInput: CoreInput
{
    private bool _jumpPressed;

    public event Action JumpInputDown;
    public event Action JumpInputUp;
    
    public override Vector2 GetInput()
    {
        var input = Vector2.Zero;

        var keyboard = Keyboard.GetState();
        
        //Get input from "W", "A", "S" and "D" keys and write into _rawInput variable
        input.Y += keyboard.IsKeyDown(Keys.W) ? -1f : 0f;
        input.Y += keyboard.IsKeyDown(Keys.S) ? 1f : 0f;
        input.X += keyboard.IsKeyDown(Keys.A)? -1f : 0f;
        input.X += keyboard.IsKeyDown(Keys.D)? 1f : 0f;

        if (_jumpPressed)
        {
            if (keyboard.IsKeyUp(Keys.Space))
            {
                _jumpPressed = false;
                JumpInputUp?.Invoke();
            }
        }
        else
        {
            if (keyboard.IsKeyDown(Keys.Space))
            {
                _jumpPressed = true;
                JumpInputDown?.Invoke();
            }
        }
        
        
        return input;
    }
}