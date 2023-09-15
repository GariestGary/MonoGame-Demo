namespace Demo.Source.Game;

using Microsoft.Xna.Framework;
using Core;

public abstract class Character
{
    private Transform _t;
    private Rectangle _rect;
    private CoreMovement _movement;
    private CoreInput _input;

    public Transform T => _t;
    public Rectangle Rect => _rect;

    public Character(Vector2 size)
    {
        _rect = new Rectangle(0, 0, (int) size.X, (int) size.Y);
        _t = new Transform();
    }

    public void SetMovement(CoreMovement movement)
    {
        _movement = movement;
        _movement.Initialize(_t);
    }
    
    public  void SetInput(CoreInput input)
    {
        _input = input;
    }
    
    public void Update(float delta)
    {
        if (_input == null || _movement == null)
        {
            return;
        }
        
        _movement.ProvideInput(_input.GetInput());
        _movement.Update(delta);
        _rect = _movement.CalculateRect(_rect);
        _movement.ResolveCollisions();
        UpdateInternal(delta);
    }

    protected virtual void UpdateInternal(float delta)
    {
        
    }
}