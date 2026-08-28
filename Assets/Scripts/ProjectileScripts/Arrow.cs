using UnityEngine;

public class Arrow : Projectile
{
    protected override void Awake()
    {
        base.Awake();
        speed = 25f;
    }
}
