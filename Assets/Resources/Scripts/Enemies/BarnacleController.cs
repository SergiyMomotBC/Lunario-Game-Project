using UnityEngine;

public class BarnacleController : MonoBehaviour
{
	public float delay = 0.0f;

	private const float VERTICAL_SPEED = 3.0f;
	private const float STAY_ON_TOP_TIME = 2.0f;

	private Transform barnacle;
	private Vector3 barnacleOrigin;
	private Vector3 tubeTop;
	private Vector3 targetPos;
	private bool isMoving;
	private float timer;

	public void Awake() 
	{
		barnacle = transform.FindChild("Barnacle");
		barnacle.GetComponent<SpriteRenderer> ().sortingOrder = -1;
		barnacle.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
		barnacleOrigin = barnacle.position;
		tubeTop = new Vector3 (transform.position.x, transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y - 0.13f, 0.0f);
        timer = Time.time;
        this.enabled = false;
        targetPos = barnacleOrigin;
        Invoke("EnableUpdate", delay);
	}

	public void Update() 
	{
		if (isMoving) 
		{			 
			barnacle.position = Vector3.MoveTowards (barnacle.position, targetPos, VERTICAL_SPEED * Time.deltaTime);

			if (barnacle.position == targetPos) {
				isMoving = false;
                timer = Time.time;
			}
		} 
		else if (Time.time - timer >= STAY_ON_TOP_TIME) {
		    isMoving = true;
		    targetPos = targetPos == barnacleOrigin ? tubeTop : barnacleOrigin;
		}
	}

    private void EnableUpdate() { this.enabled = true; }
}
