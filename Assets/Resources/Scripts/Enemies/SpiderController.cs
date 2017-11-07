using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public float height = 3.0f;
    public float speed = 3.0f;
    public float waitInterval = 2.0f;

    private Animator anim;
    private Vector3 origin;
    private Vector3 bottom;
    private Vector3 target;
    private LineRenderer net;
    private Vector3 netOrigin;
    private float halfHeight;
    private float timer;
    private bool isWaiting;

    public void Awake()
    {
        anim = GetComponent<Animator>();

        netOrigin = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

        var netDrawer = new GameObject("Net");
        netDrawer.transform.parent = transform;
        netDrawer.transform.position = netOrigin;

        net = netDrawer.AddComponent<LineRenderer>();
        net.material = new Material(Shader.Find("Particles/Additive"));
        net.startColor = Color.white;
        net.endColor = Color.white;
        net.startWidth = 0.01f;
        net.endWidth = 0.01f;
        net.SetPosition(0, netOrigin);
        net.SetPosition(1, transform.position);

        halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        origin = transform.position;
        bottom = new Vector3(origin.x, origin.y - height, origin.z);
        target = bottom;

        anim.SetBool("isMoving", true);
    }
	
	public void Update()
    {
        if (isWaiting)
        {
            if (Time.time - timer >= waitInterval) {
                isWaiting = false;
                anim.SetBool("isMoving", true);
            }

            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
            target = target == origin ? bottom : origin;

        var end = new Vector3(transform.position.x, transform.position.y - halfHeight, transform.position.z);
        net.SetPosition(1, end);

        if (transform.position == origin)
        {
            isWaiting = true;
            timer = Time.time;
            anim.SetBool("isMoving", false);
        }
    }
}
