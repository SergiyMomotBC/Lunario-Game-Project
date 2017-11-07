using UnityEngine;

public class SlimeController : MonoBehaviour, IHurtable
{
    private Animator anim;
    
	public void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
    public void OnHurt()
    {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
        anim.SetTrigger("isDead");
        Destroy(gameObject, 1.5f);
    }
}
