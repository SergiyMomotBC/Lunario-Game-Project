using UnityEngine;

public class EnemyCollisionResponder : MonoBehaviour
{
    private IHurtable enemyController;
    private bool wasCollisionDisabled;
    private CapsuleCollider2D playerCollider;
    private CapsuleCollider2D enemyCollider;
    private PlayerLifeController playerLife;
    private AudioSource sound;

	public void Start()
    {
        sound = GetComponent<AudioSource>();
        enemyController = GetComponent<IHurtable>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        var player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        playerLife = player.GetComponent<PlayerLifeController>();
	}

	public void Update()
    {
        if (wasCollisionDisabled)
        {
            var playerBounds = playerCollider.bounds;
            playerBounds.size = new Vector3(playerBounds.size.x + 0.05f, playerBounds.size.y + 0.05f, 0.0f);
            if (!playerBounds.Intersects(enemyCollider.bounds))
            {
                Physics2D.IgnoreCollision(enemyCollider, playerCollider, false);
                wasCollisionDisabled = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (CollisionHelper.isCollisionFromAbove(collision.transform, transform))
                OnEnemyHurt(collision.gameObject);
            else if(gameObject.CompareTag("Enemy"))
            {
                playerLife.WasHurt();
                Physics2D.IgnoreCollision(enemyCollider, collision.collider, true);
                wasCollisionDisabled = true;
            }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            if (CollisionHelper.isCollisionFromAbove(other.transform, transform))
                OnEnemyHurt(other.gameObject);
            else
                playerLife.WasHurt();
    }

    private void OnEnemyHurt(GameObject player)
    {
        sound.Play();
        var playerBody = player.GetComponent<Rigidbody2D>();
        playerBody.velocity = new Vector2(playerBody.velocity.x, 0.0f);
        playerBody.AddForce(new Vector2(0.0f, 300.0f));
        if (enemyController != null)
            enemyController.OnHurt();
    }
}
