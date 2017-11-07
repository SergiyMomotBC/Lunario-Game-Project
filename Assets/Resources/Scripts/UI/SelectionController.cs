using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionController : MonoBehaviour
{
    public LevelSelectionPoint firstPoint;

    private LevelSelectionPoint currentPoint;
    private bool isMoving;
    private LevelSelectionPoint target;
    private const float speed = 4.0f;

	public void Awake()
    {
        Time.timeScale = 1.0f;

        currentPoint = firstPoint;

        transform.position = new Vector2(
            currentPoint.transform.position.x, 
            currentPoint.transform.position.y
        );
	}
	
	public void Update()
    {
	    if(!isMoving)
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetButton("Submit") && currentPoint.level != 0) {
                Game.nextLevel = currentPoint.level;
                SceneManager.LoadScene("Loading");
            }

            if(horizontal > 0.5f && currentPoint.right != null) {
                target = currentPoint.right.GetComponent<LevelSelectionPoint>();
                isMoving = true;
            } else if (horizontal < -0.5f && currentPoint.left != null) {
                target = currentPoint.left.GetComponent<LevelSelectionPoint>();
                isMoving = true;
            } else if (vertical > 0.5f && currentPoint.up != null) {
                target = currentPoint.up.GetComponent<LevelSelectionPoint>();
                isMoving = true;
            } else if (vertical < -0.5f && currentPoint.down!= null) {
                target = currentPoint.down.GetComponent<LevelSelectionPoint>();
                isMoving = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            if (transform.position == target.transform.position)
            {
                isMoving = false;
                currentPoint = target;
                target = null;
            }
        }
	}
}
