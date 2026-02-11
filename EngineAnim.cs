using Godot;
using System;

public partial class EngineAnim : AnimatedSprite2D
{
    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("force_foreward"))
        {
            Play("Engine");
        }
        else
        {
            Stop();
        }
    }
}
