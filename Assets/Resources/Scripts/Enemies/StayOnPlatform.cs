using UnityEngine;

public class StayOnPlatform : MonoBehaviour
{
    public LayerMask groundLayers;

    private float halfWidth;

	public void Start()
    {
        halfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
	}
	
	public void Update()
    {
        var origin = transform.right == Vector3.right ?
            new Vector2(transform.position.x - halfWidth, transform.position.y) :
            new Vector2(transform.position.x + halfWidth, transform.position.y);

        var extraOrigin = transform.right == Vector3.right ?
            new Vector2(transform.position.x - halfWidth - 0.05f, transform.position.y) :
            new Vector2(transform.position.x + halfWidth + 0.05f, transform.position.y);

        bool isOnPlatform = Physics2D.Raycast(origin, Vector2.down, 0.1f, groundLayers);

        if(!isOnPlatform && !Physics2D.Raycast(extraOrigin, Vector2.down, 0.1f, groundLayers))
        {

            var currentRotation = transform.eulerAngles;
            currentRotation.y += 180;
            transform.eulerAngles = currentRotation;
        }
	}
}
