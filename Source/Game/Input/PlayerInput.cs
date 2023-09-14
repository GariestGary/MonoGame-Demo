using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Demo.Source.Game.Input;

public class PlayerInput
{
    private Vector2 _rawInput = Vector2.Zero;
    
    public Vector2 RawInput => _rawInput;
    public Vector2 NormalizedInput
    {
        get
        {
            float length = (float)Math.Sqrt(_rawInput.X * _rawInput.X + _rawInput.Y * _rawInput.Y);

            Vector2 normalized = new Vector2(_rawInput.X / length, _rawInput.Y / length);
            
            return normalized;
        }
    }

    public void Update()
    {
        //Get input from "W", "A", "S" and "D" keys and write into _rawInput variable
        _rawInput.Y += Keyboard.GetState().IsKeyDown(Keys.W) ? 1f : 0f;
        _rawInput.Y += Keyboard.GetState().IsKeyDown(Keys.S) ? -1f : 0f;
        _rawInput.X += Keyboard.GetState().IsKeyDown(Keys.A)? -1f : 0f;
        _rawInput.X += Keyboard.GetState().IsKeyDown(Keys.D)? 1f : 0f;
    }
}