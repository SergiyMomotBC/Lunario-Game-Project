using UnityEngine;

public class MagnetPowerUp : PowerUp
{
    private float radiusOfMagnet = 5.0f;
    private LayerMask collectiblesMask;

    protected override void OnStart()
    {
        collectiblesMask = 1 << LayerMask.NameToLayer("Collectibles");
    }

    protected override void OnTimerEnd() {}

    protected override void OnUpdate()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, radiusOfMagnet, collectiblesMask);

        foreach(var go in colliders)
        {
            go.gameObject.layer = 0;
            go.gameObject.AddComponent<MoveTowardsPlayer>();
        }
    }
}

