using UnityEngine;

public class ItemBoxController : MonoBehaviour 
{
    public Sprite disabledSprite;
    public GameObject whatToSpawnWhenHit;

    private SpriteRenderer spriteRenderer;
    private AudioSource sound;

    public void Awake()
    {
        sound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
		    if (CollisionHelper.isCollisionFromBellow(other.transform, transform))
                WasHit();
    }

    private void WasHit()
    {
        sound.Play();

        if (whatToSpawnWhenHit != null)
            Instantiate(
                whatToSpawnWhenHit, 
                new Vector3(transform.position.x, transform.position.y + spriteRenderer.bounds.size.y, transform.position.z), 
                whatToSpawnWhenHit.transform.rotation
            );

        spriteRenderer.sprite = disabledSprite;
        Destroy(this);
    }
}
