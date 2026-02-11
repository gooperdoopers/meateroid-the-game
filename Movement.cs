using Godot;
using System;

public partial class Movement : RigidBody2D
{
    float torqueSpeed = 6500f * 1000f;
    float maxTorque = 0f;
    float forceSpeed = 1500f;
    float maxSpeed = 350f;
    public override void _Process(double delta)
    {
        float forceDirection = Input.GetAxis("force_foreward", "force_back");
        GD.Print(forceDirection * forceDirection * forceSpeed * (float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        float force = Input.GetAxis("force_foreward", "force_back");
        Vector2 forceDirection = Transform.Y;
        float torqueDirection = Input.GetAxis("rot_left", "rot_right");
        ApplyTorque(torqueDirection * torqueSpeed * (float)delta);
        ApplyCentralImpulse(force * forceDirection * forceSpeed * (float)delta);
        
    }

    public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        Vector2 linearVelocity = state.LinearVelocity;
        if (linearVelocity.Length() > maxSpeed)
        {
            state.LinearVelocity = linearVelocity.Normalized() * maxSpeed;
        }
    }

}
