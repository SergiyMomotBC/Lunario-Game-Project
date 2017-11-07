using UnityEngine;

public class BeeController : MonoBehaviour
{
    public float speed = -3.0f;
    private Rigidbody2D body;
    private Transform cameraTransform;

	public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        cameraTransform = Camera.main.transform;
	}
	
	void FixedUpdate()
    {
        if (cameraTransform.position.x - transform.position.x >= 15.0f)
            Destroy(gameObject);

        body.velocity = new Vector2(speed, body.velocity.y);
	}
}
