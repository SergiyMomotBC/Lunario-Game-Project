using UnityEngine;

public class BatController : MonoBehaviour
{
    public float triggerDistance;
    public float speed;

    private bool isHanging;
    private Animator animator;
    private Transform player;
    private Vector3 hangPosition;
    private SpriteRenderer sprite;

	public void Awake()
    {
        isHanging = true;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hangPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
	}
	
	public void Update ()
    {
        var distance = Vector2.Distance(transform.position, player.position);

        if (isHanging)
        {
            if (distance <= triggerDistance) {
                isHanging = false;
                animator.SetBool("isHanging", isHanging);
            }
        }
        else
        {
            var targetPos = distance <= triggerDistance ? player.position : hangPosition;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            sprite.flipX = targetPos.x > transform.position.x;
               
            if (transform.position == hangPosition) {
                isHanging = true;
                animator.SetBool("isHanging", isHanging);
            }
        }
	}
}