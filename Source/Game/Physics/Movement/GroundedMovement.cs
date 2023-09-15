using Microsoft.Xna.Framework;

namespace Demo.Source.Game;

public class GroundedMovement: CoreMovement
{
    public float Gravity { get; set; } = 200;
    public float FallMultiplier { get; set; } = 1.06f;
    public float MaxJumpvelocity { get; set; } = -1000;
    public float MinJumpVelocity { get; set; } = -250;
    private bool _jumped;
    private bool _jumpedPrevFrame;

    protected override Vector2 CalculateVelocity(Vector2 currentVelocity, Vector2 rawInput, float deltaTime)
    {
        float xVelocity = rawInput.X * MovementSpeed;
        float yVelocity = currentVelocity.Y + Gravity * deltaTime;

        if (_t.Position.Y >= 500)
        {
            _t.Position -= Vector2.UnitY * (_t.Position.Y - 500);
            yVelocity = 0;
        }

        if (currentVelocity.Y >= 0 && _jumped && !_jumpedPrevFrame)
        {
            //_t.Position -= Vector2.UnitY * 10;
            yVelocity = MaxJumpvelocity;
        }

        if (_jumpedPrevFrame && !_jumped && currentVelocity.Y < MinJumpVelocity)
        {
            yVelocity = MinJumpVelocity;
        }

        if (yVelocity >= 0)
        {
            _jumped = false;
        }
        
        _jumpedPrevFrame = _jumped;

        if (yVelocity >= 0)
        {
            yVelocity *= FallMultiplier;
        }
        
        return new Vector2(xVelocity, yVelocity);
    }

    public void OnJumpInputDown()
    {
        _jumped = true;
    }

    public void OnJumpInputUp()
    {
        _jumped = false;
    }
}