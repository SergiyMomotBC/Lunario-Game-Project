using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    public Sprite[] cloudSprites;

    private float timer;
    private float interval;
    private float height;
    private int cloudIndex;
    private int sortingLayerOfBackground;
    private float halfCameraWidth;

	public void Awake()
    {
        halfCameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        sortingLayerOfBackground = GetComponent<SpriteRenderer>().sortingLayerID;

        for (var x = -halfCameraWidth; x < halfCameraWidth; x += 3.0f)
            InstantiateCloud(new Vector2(x, Random.value * 3 + 2.0f));
    }

    public void Start()
    {
        Randomize();
        timer = Time.time;
    }

    public void Update()
    {
	    if(Time.time - timer >= interval)
        {
            InstantiateCloud(
                new Vector2(Camera.main.transform.position.x + halfCameraWidth + 1.0f, height)
            );
            Randomize();
            timer = Time.time;
        }
	}

    private void InstantiateCloud(Vector2 position)
    {
        var cloud = new GameObject("Cloud");
        cloud.transform.parent = transform;
        cloud.transform.position = position;

        var spriteRenderer = cloud.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = cloudSprites[cloudIndex];
        spriteRenderer.sortingLayerID = sortingLayerOfBackground;

        cloud.AddComponent<MoveCloud>();
    }

    private void Randomize()
    {
        cloudIndex = Random.Range(0, cloudSprites.Length - 1);
        height = Random.value * 3 + 2.0f;
        interval = Random.value * 8 + 4.0f;
    }
}