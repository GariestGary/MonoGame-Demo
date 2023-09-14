using Demo.Source.Utils;
using Microsoft.Xna.Framework;

namespace Demo.Source.Game.Player;

public class Player: Character
{
    private Vector2 _rawInput;
    
    public Player(Vector2 size) : base(size)
    {
    }

    public void ProvideInput(Vector2 input)
    {
        _rawInput = input;
    }

    protected override Vector2 ProcessPosition(Vector2 position, float deltaTime)
    {
        var newPos = position;
        var (x, y) = _rawInput.Normalized();
        
        newPos.X += x * deltaTime * MovementSpeed;
        newPos.Y += y * deltaTime * MovementSpeed;
        
        return newPos;
    }
}