using UnityEngine;

public class Bullet : Projectile
{
    protected override void Awake()
    {
        base.Awake();
        speed = 50f;
    }
}
