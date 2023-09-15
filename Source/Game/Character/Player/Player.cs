namespace Demo.Source.Game;

using Utils;
using Microsoft.Xna.Framework;

public class Player: Character
{
    private Vector2 _rawInput;
    
    private PlayerInput _input;
    private GroundedMovement _movement;
    
    public Player(Vector2 size) : base(size)
    {
        _input = new PlayerInput();
        _movement = new GroundedMovement() { Gravity = 1750f, MovementSpeed = 500f };

        _input.JumpInputDown += () => _movement.OnJumpInputDown();
        _input.JumpInputUp += () => _movement.OnJumpInputUp();
        
        SetInput(_input);
        SetMovement(_movement);
    }
}