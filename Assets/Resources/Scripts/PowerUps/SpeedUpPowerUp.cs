using UnityEngine;

public class SpeedUpPowerUp : PowerUp
{
    private PlayerController player;
    private Animator anim;

    protected override void OnStart()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        player.horizontalSpeed *= 2.0f;
        anim.speed *= 2.0f;
    }

    protected override void OnTimerEnd()
    {
        player.horizontalSpeed /= 2.0f;
        anim.speed /= 2.0f;
    }
   
    protected override void OnUpdate() {}
}
