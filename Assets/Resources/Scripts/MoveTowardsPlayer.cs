using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    private Transform player;
    private const float SPEED = 16.0f;

	public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, SPEED * Time.deltaTime);
	}
}
