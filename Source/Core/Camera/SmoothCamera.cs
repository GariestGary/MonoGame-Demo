using System;
using Comora;
using Demo.Source.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace Demo.Source.Core;

public class SmoothCamera: Camera
{
    private Transform _target;
    private float _smoothTime;
    private float _xReference;
    private float _yReference;

    public SmoothCamera(GraphicsDevice device, Transform target, float smoothTime) : base(device)
    {
        _smoothTime = smoothTime;
        _target = target;
    }

    public void UpdateSmoothedPosition(float deltaTime)
    {
        var newPos = Position;
        
        newPos.X = MathExtended.SmoothDamp(Position.X, _target.Position.X, ref _xReference, _smoothTime, float.PositiveInfinity, deltaTime);
        newPos.Y = MathExtended.SmoothDamp(Position.Y, _target.Position.Y, ref _yReference, _smoothTime, float.PositiveInfinity, deltaTime);
        
        Position = newPos;
    }
}