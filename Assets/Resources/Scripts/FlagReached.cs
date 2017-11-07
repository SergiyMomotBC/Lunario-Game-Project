using UnityEngine;

public class FlagReached : MonoBehaviour 
{
    public Sprite reachedSprite;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(GetComponent<Animator>());
            Destroy(GetComponent<EdgeCollider2D>());
            GetComponent<SpriteRenderer>().sprite = reachedSprite;
            GetComponent<AudioSource>().Play();
            //save restore spot
            Destroy(this);
        }         
    }
}
