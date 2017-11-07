using UnityEngine;

public class PiranhaController : MonoBehaviour
{
    public float jumpInterval = 1.5f;

    private float yOrigin;
    private float timer;
    private bool isJumping;
    private Rigidbody2D body;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        yOrigin = transform.position.y;
        timer = Time.time;
	}
	
	public void Update()
    {
	    if(isJumping)
        {
            if(transform.position.y <= yOrigin)
            {
                transform.position = new Vector3(transform.position.x, yOrigin, 0.0f);
                body.velocity = Vector2.zero;
                body.gravityScale = 0.0f;
                timer = Time.time;
                isJumping = false;
                FlipY();
            }

            if (body.velocity.y < 0.0f && transform.localScale.y > 0.0f)
                FlipY();
        }
        else
        {
            if(Time.time - timer >= jumpInterval)
            {
                isJumping = true;
                body.gravityScale = 1.0f;
                body.AddForce(new Vector2(0.0f, 600.0f));
            }
        }
	}

    private void FlipY()
    {
        var scale = transform.localScale;
        scale.y = transform.localScale.y * -1;
        transform.localScale = scale;
    }
}
