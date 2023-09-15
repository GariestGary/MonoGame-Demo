using System;
using Demo.Source.Core.World;
using Demo.Source.Utils;

namespace Demo.Source.Game;

using Microsoft.Xna.Framework;
using Core;

public abstract class CoreMovement
{
    public float MovementSpeed;
    public Vector2 Velocity;
    public Vector2 VelocityDampTime;
    
    protected Transform _t;
    protected Rectangle _rect = new Rectangle();

    protected Vector2 _input;

    public void Initialize(Transform t)
    {
        _t = t;
    }
    
    public void Update(float deltaTime)
    {
        Velocity = CalculateVelocity(Velocity, _input, deltaTime);
        _t.Position += Velocity * deltaTime;
    }

    public void ProvideInput(Vector2 input)
    {
        _input = input;
    }

    protected abstract Vector2 CalculateVelocity(Vector2 currentVelocity, Vector2 rawInput, float deltaTime);

    public Rectangle CalculateRect(Rectangle rect)
    {
        _rect.Width = rect.Width;
        _rect.Height = rect.Height;
        _rect.X = (int) Math.Round(_t.Position.X, 0);
        _rect.Y = (int) Math.Round(_t.Position.Y, 0);
        
        return _rect;
    }

    public void ResolveCollisions()
    {
        var collision = Physics.GetCollision(_rect);
        
        if(collision)
        {
            var oppositeDir = new Vector2(_rect.X, _rect.Y) - new Vector2(collision.collidedRect.X, collision.collidedRect.Y);
            var point = oppositeDir.Normalized() * collision.collisionDepth;

            _rect.X = (int) Math.Round(point.X, 0);
            _rect.Y = (int) Math.Round(point.Y, 0);
        }
    }
}