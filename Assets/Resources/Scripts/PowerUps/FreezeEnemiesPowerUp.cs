using UnityEngine;

public class FreezeEnemiesPowerUp : PowerUp
{
    private FreezeEnemy[] enemies;

    protected override void OnStart()
    {
        enemies = GameObject.FindObjectsOfType<FreezeEnemy>();

        foreach (var enemy in enemies)
            enemy.Freeze();
    }

    protected override void OnTimerEnd()
    {
        foreach (var enemy in enemies)
            enemy.Unfreeze();
    }

    protected override void OnUpdate() {}
}

