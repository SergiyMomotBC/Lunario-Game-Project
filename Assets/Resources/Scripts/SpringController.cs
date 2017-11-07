using UnityEngine;

public class SpringController : MonoBehaviour 
{
    public Sprite normal;
    public Sprite pushed;

    private SpriteRenderer spriteRenderer;
    private AudioSource sound;

	public void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
	}
	
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            if (CollisionHelper.isCollisionFromAbove(collision.transform, transform))
            {
                spriteRenderer.sprite = pushed;
                Invoke("ChangeBackToNormal", 0.1f);

                var body = collision.rigidbody;
                body.velocity = new Vector2(body.velocity.x, 0.0f);
                body.AddForce(new Vector2(0.0f, 1000.0f));

                sound.Play();
            }
    }

    private void ChangeBackToNormal()
    {
        spriteRenderer.sprite = normal;
    }
}
