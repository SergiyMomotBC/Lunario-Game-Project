using UnityEngine;

public class MoveOnPlatform : MonoBehaviour
{
    public float speed = -2.0f;

    private Rigidbody2D body;
  
    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if(body != null)
            body.velocity = new Vector2(transform.right == Vector3.right ? speed : -speed, body.velocity.y);
    }
}
