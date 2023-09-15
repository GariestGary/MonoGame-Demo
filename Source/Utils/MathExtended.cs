namespace Demo.Source.Utils;

using Microsoft.Xna.Framework;
using System;

public static class MathExtended
{
    public static Vector2 Normalized(this Vector2 vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

        Vector2 normalized = vector;

        if (normalized.X != 0)
        {
            normalized.X /= length;
        }
            
        if (normalized.Y!= 0)
        {
            normalized.Y /= length;
        }
            
        return normalized;
    }
    
    public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = float.PositiveInfinity, float deltaTime = 0.001f)
    {
        // Based on Game Programming Gems 4 Chapter 1.10
        smoothTime = Math.Max(0.0001F, smoothTime);
        float omega = 2F / smoothTime;

        float x = omega * deltaTime;
        float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
        float change = current - target;
        float originalTo = target;

        // Clamp maximum speed
        float maxChange = maxSpeed * smoothTime;
        change = Math.Clamp(change, -maxChange, maxChange);
        target = current - change;

        float temp = (currentVelocity + omega * change) * deltaTime;
        currentVelocity = (currentVelocity - omega * temp) * exp;
        float output = target + (change + temp) * exp;

        // Prevent overshooting
        if (originalTo - current > 0.0F == output > originalTo)
        {
            output = originalTo;
            currentVelocity = (output - originalTo) / deltaTime;
        }

        return output;
    }
}