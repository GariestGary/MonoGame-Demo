namespace Demo.Source.Game;

using Microsoft.Xna.Framework;
using Utils;

public class BasicDirectionalMovement: CoreMovement
{
    protected override Vector2 CalculateVelocity(Vector2 currentVelocity, Vector2 rawInput, float deltaTime)
    {
        var (x, y) = rawInput.Normalized();
        
        currentVelocity.X = x * deltaTime * MovementSpeed;
        currentVelocity.Y = y * deltaTime * MovementSpeed;
        
        return currentVelocity;
    }
}