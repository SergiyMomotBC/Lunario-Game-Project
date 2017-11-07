using UnityEngine;

public class FallDownOnDeath : MonoBehaviour, IHurtable
{
    private Animator anim;
    private bool isDead;

    public void Awake()
    {
        anim = GetComponent<Animator>();

        var colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
            if (!collider.isTrigger)
                collider.isTrigger = true;
    }

	public void Update()
    {
        if (transform.position.y < -6.0f)
            Destroy(gameObject);
    }

    public void OnHurt()
    {
        if (isDead) return;

        anim.SetTrigger("isDead");

        var body = GetComponent<Rigidbody2D>();
        if(body == null)
            body = gameObject.AddComponent<Rigidbody2D>();
        body.gravityScale = 3.0f;
        body.velocity = Vector2.zero;

        isDead = true;
    }
}
