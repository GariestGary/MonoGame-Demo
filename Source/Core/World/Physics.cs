using System.Collections.Generic;
using Demo.Source.Utils;
using Microsoft.Xna.Framework;

namespace Demo.Source.Core.World;

public static class Physics
{
    private static List<Rectangle> _obstacles = new();
    
    public static void AddObstacle(Rectangle obstacle)
    {
        _obstacles.Add(obstacle);
    }
    
    public static void RemoveObstacle(Rectangle obstacle)
    {
        _obstacles.Remove(obstacle);
    }

    public static CollisionInfo GetCollision(Rectangle rect)
    {
        var collision = new CollisionInfo();
        
        for (int i = 0; i < _obstacles.Count; i++)
        {
            var depth = rect.GetIntersectionDepth(_obstacles[i]);
            
            if (depth != Vector2.Zero)
            {
                collision.collisionDepth = depth;
                collision.collidedRect = _obstacles[i];
                return collision;
            }
        }
        
        return collision;
    }
}

public struct CollisionInfo
{
    public Rectangle collidedRect;
    public Vector2 collisionDepth;
    
    public static implicit operator bool(CollisionInfo info) => info.collisionDepth  != Vector2.Zero;
}