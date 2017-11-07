using UnityEngine;

public class CollectableController : MonoBehaviour 
{
    public CollectableItems type;

    private PlayerCollectedItems player;
    private AudioSource audioPlayer;
    private SpriteRenderer sprite;

	public void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectedItems>();
        audioPlayer = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.ItemCollected(type);
            audioPlayer.Play();
            sprite.enabled = false;
            Destroy(gameObject, audioPlayer.clip.length + 0.1f);
            Destroy(this);
        }
    }
}