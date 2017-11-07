using UnityEngine;

public class FlyController : MonoBehaviour
{
    public float radius;
    public float speed;

    private Vector3 left;
    private Vector3 right;
    private Vector3 target;
    private SpriteRenderer sprite;
	
	public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        left = new Vector3(transform.position.x - radius, transform.position.y, 0.0f);
        right = new Vector3(transform.position.x + radius, transform.position.y, 0.0f);
        target = left;
	}
	
	public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            sprite.flipX = !sprite.flipX;
            target = target == left ? right : left;
        }
	}
}
