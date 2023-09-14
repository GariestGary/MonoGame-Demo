using Microsoft.Xna.Framework;

namespace Demo.Source.Core;

public class Transform
{
    private Vector2 _position;
    private Vector2 _scale;
    private float _rotation;
    
    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public Vector2 Scale
    {
        get => _scale;
        set => _scale = value;
    }
    public float RotationAngle { get; set; }
}