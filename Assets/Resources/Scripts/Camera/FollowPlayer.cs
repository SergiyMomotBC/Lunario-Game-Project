using UnityEngine;

public class FollowPlayer : MonoBehaviour 
{
    private Transform player;
    private Camera cam;
    private float endPosition;

	public void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
        endPosition = GameObject.Find("Level End").transform.position.x - cam.orthographicSize * cam.aspect;
	}
	
	public void Update() 
    {  
        var viewportPos = cam.WorldToViewportPoint(player.position);

        if (viewportPos.x > 0.5f)
            transform.Translate(new Vector3(player.position.x - transform.position.x, 0.0f, 0.0f));
        else if (viewportPos.x < 0.25f && transform.position.x > 0.0f)
            transform.Translate(
                new Vector3(player.position.x - transform.position.x + 0.5f * cam.orthographicSize * cam.aspect, 0.0f, 0.0f)
            );

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.0f, endPosition), transform.position.y, transform.position.z);
    }
}
