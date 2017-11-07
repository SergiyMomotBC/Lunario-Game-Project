using UnityEngine;

public class MouseController : MonoBehaviour, IHurtable
{
    public float speed = 3.75f;

    private Transform player;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
	
	public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	public void Update()
    {
        if (body == null)
            return;

        if (Mathf.Abs(player.position.x - transform.position.x) >= 0.1f)
        { 
            body.velocity = new Vector2(player.position.x > transform.position.x ? speed : -speed, body.velocity.y);
            sprite.flipX = player.position.x > transform.position.x;
        }
        else
            body.velocity = new Vector2(0.0f, body.velocity.y);
    }

    public void OnHurt()
    {
        gameObject.AddComponent<FallDownOnDeath>().OnHurt();
        Destroy(this);
    }
}
