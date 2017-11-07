using UnityEngine;

public class ObstacleResponder : MonoBehaviour
{
    public LayerMask obstacleLayers;

    private float halfSpriteWidth;

	public void Awake()
    {
        halfSpriteWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
	}
	
	public void Update()
    {
        var wasObstacleHit = Physics2D.Raycast(
            new Vector2(transform.position.x - halfSpriteWidth * transform.right.x, transform.position.y + 0.2f), 
            transform.right * -1, 0.05f, obstacleLayers
        );

        if (wasObstacleHit)
        {
            var currentRotation = transform.eulerAngles;
            currentRotation.y += 180;
            transform.eulerAngles = currentRotation;
        }
    }
}
