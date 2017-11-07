using UnityEngine;

public class DestroyBox : MonoBehaviour 
{
    private const byte PARTICLES_TO_EMIT = 8;

    private ParticleSystem particles;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    private AudioSource sound;
    private bool wasDestroyed;

	public void Start()
    {
        sound = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
		    if (CollisionHelper.isCollisionFromBellow(other.transform, transform))
                WasDestroyed();
    }

    public void Update()
    {
        if (wasDestroyed && !particles.isPlaying)
            Destroy(gameObject);
    }

    private void WasDestroyed()
    {
        sound.Play();
        particles.Emit(PARTICLES_TO_EMIT);
        sprite.enabled = false;
        boxCollider.enabled = false;
        wasDestroyed = true;
    }
}
