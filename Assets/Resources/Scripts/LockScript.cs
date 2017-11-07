using UnityEngine;

public enum LockColor
{
    Red, Green, Blue, Yellow
}

public class LockScript : MonoBehaviour 
{
    public LockColor color;
    public GameObject whatToSpawnOnUnlock;

    private AudioSource sound;
    private SpriteRenderer sprite;
    private BoxCollider2D box;
    private PlayerCollectedItems player;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        box = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectedItems>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && CollisionHelper.isCollisionFromBellow(other.transform, transform))    
            switch (color)
            {
                case LockColor.Red:
                    if (player.redKey)
                        Unlock();
                    break;
                case LockColor.Green:
                    if (player.greenKey)
                        Unlock();
                    break;
                case LockColor.Blue:
                    if (player.blueKey)
                        Unlock();
                    break;
                case LockColor.Yellow:
                    if (player.yellowKey)
                        Unlock();
                    break;
            }
    }

    private void Unlock()
    {
        sound.Play();
        sprite.enabled = false;
        box.enabled = false;

        if(whatToSpawnOnUnlock != null)
            Instantiate(whatToSpawnOnUnlock, new Vector3(transform.position.x, transform.position.y + 0.25f, 0.0f), Quaternion.identity);

        Destroy(gameObject, sound.clip.length + 0.1f);
    }
}
