using UnityEngine;

public class InvinciblePowerUp : PowerUp
{
    private GameObject bubble;
    private PlayerLifeController life;

    protected override void OnStart()
    {
        bubble = Instantiate(Resources.Load("Prefabs/Bubble") as GameObject);
        bubble.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + GetComponent<SpriteRenderer>().bounds.extents.y,
            transform.position.z
        );
        bubble.transform.parent = transform;

        life = GetComponent<PlayerLifeController>();
        life.isInvincible = true;
	}
	
	protected override void OnUpdate() {}

    protected override void OnTimerEnd()
    {
        life.isInvincible = false;
        Destroy(bubble);
    }
}