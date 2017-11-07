using UnityEngine;

public class SnailController : MonoBehaviour, IHurtable
{
    private bool isInShell;
    private Animator anim;
    private Rigidbody2D body;
    private float timer;
    private const float STAY_IN_SHELL = 3.0f;

	public void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
	}
	
	public void Update()
    {
	    if(isInShell && Time.time - timer >= STAY_IN_SHELL)
        {
            isInShell = false;
            gameObject.tag = "Enemy";
            anim.SetBool("isInShell", isInShell);
            body.constraints = ~RigidbodyConstraints2D.FreezePositionX;
        }
	}

    public void OnHurt()
    {
        if(!isInShell)
        {
            isInShell = true;
            gameObject.tag = "Untagged";
            anim.SetBool("isInShell", isInShell);
            timer = Time.time;
            body.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            var fallDeath = gameObject.AddComponent<FallDownOnDeath>();
            Destroy(GetComponent<StayOnPlatform>());
            fallDeath.OnHurt();
            Destroy(this);
        }
    }
}
