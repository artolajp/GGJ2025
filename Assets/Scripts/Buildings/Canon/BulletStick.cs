using UnityEngine;

public class BulletStick : Bullet
{
    protected override void Start()
    {
        Vector3 direction = transform.forward;

        rigidBody.linearVelocity = direction * speed;
    }

    public override void TeleportParticles()
    {
        base.TeleportParticles();

        Vector3 direction = transform.forward;

        rigidBody.linearVelocity = direction * speed;
    }
}
