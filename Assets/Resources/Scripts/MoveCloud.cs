using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    public float speed = 0.5f;

    private float targetPositionX;
    private Vector3 target;

	public void Awake()
    {
        targetPositionX = Camera.main.orthographicSize * Camera.main.aspect * -1 - 1.0f;
        target = new Vector3(0.0f, transform.position.y, 0.0f);
	}
	
	public void Update()
    {
        target.x = Camera.main.transform.position.x + targetPositionX;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x <= Camera.main.transform.position.x + targetPositionX)
            Destroy(gameObject);
	}
}