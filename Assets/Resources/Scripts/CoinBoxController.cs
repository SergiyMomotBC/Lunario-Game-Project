using UnityEngine;

public class CoinBoxController : MonoBehaviour 
{
    public Sprite emptyBoxSprite;

    private const int MIN_COINS = 3;
    private const int MAX_COINS = 7;

    private int coinsLeft;
    private Animation animator;
    private PlayerCollectedItems player;
    private AudioSource sound;
    private SpriteRenderer sprite;

	public void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectedItems>();
        animator = transform.parent.GetComponent<Animation>();
        sound = GetComponent<AudioSource>();
		coinsLeft = Random.Range(MIN_COINS, MAX_COINS);
        sprite = GetComponent<SpriteRenderer>();
	}
	
	public void Update() 
    {
        if (coinsLeft <= 0 && !animator.isPlaying)
        {
            Destroy(animator);
            sprite.sprite = emptyBoxSprite;
            Destroy(this);
        }
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {          
        if (collision.gameObject.CompareTag("Player"))
			if (CollisionHelper.isCollisionFromBellow(collision.transform, transform))
                WasHit();
    }

    private void WasHit()
    {
        if (coinsLeft > 0)
        {
            sound.Play();
            coinsLeft--;
            player.ItemCollected(CollectableItems.Coin);
            animator.Play();
        }
    }
}