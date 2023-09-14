using Demo.Source.Core.Transforms;
using Microsoft.Xna.Framework;

namespace Demo.Source.Game;

public class Character
{
    private Core.Transforms.Transform _t;
    private Rectangle _rect;

    public Core.Transforms.Transform T
    {
        get => _t;
        set => _t = value;
    }

    public Rectangle Rect
    {
        get => _rect;
        private set => _rect = value;
    }
    
    public Character(Vector2 size)
    {
        _rect = new Rectangle(0, 0, (int) size.X, (int) size.Y);
        _t = new Core.Transforms.Transform();
    }
    
    public void UpdatePosition()
    {
        _rect.X = (int) T.Position.X;
        _rect.Y = (int) T.Position.Y;
    }
}