using Godot;
using System;

public partial class Movement : RigidBody2D
{
    float torque_speed = 3500 * 100;
    float force_speed = 200;
    public override void _Process(double delta)
    {
        float force_direction = Input.GetAxis("force_foreward", "force_back");
        GD.Print(force_direction * force_direction * force_speed * (float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        float force = Input.GetAxis("force_foreward", "force_back");
        Vector2 force_direction = Transform.Y;
        float rot_direction = Input.GetAxis("rot_left", "rot_right");
        ApplyTorque(rot_direction * torque_speed * (float)delta);
        ApplyCentralImpulse(force * force_direction * force_speed * (float)delta);
    }
}
