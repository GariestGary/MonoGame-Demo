using System;
using Microsoft.Xna.Framework;

namespace Demo.Source.Game;

public abstract class Character
{
    private Core.Transform _t;
    private Rectangle _rect;
    private float _movementSpeed = 250f;

    public Core.Transform T => _t;
    public Rectangle Rect => _rect;
    public float MovementSpeed => _movementSpeed;

    public Character(Vector2 size)
    {
        _rect = new Rectangle(0, 0, (int) size.X, (int) size.Y);
        _t = new Core.Transform();
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        movementSpeed = Math.Max(movementSpeed, 0);
        _movementSpeed = movementSpeed;
    }

    public void UpdatePosition(float deltaTime)
    {
        T.Position = ProcessPosition(T.Position, deltaTime);
        
        _rect.X = (int) T.Position.X;
        _rect.Y = (int) T.Position.Y;
    }

    protected abstract Vector2 ProcessPosition(Vector2 position, float deltaTime);
}