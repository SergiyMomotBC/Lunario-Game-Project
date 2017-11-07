using UnityEngine;
using System.Collections;

struct Background
{
    public Transform transform;
    public SpriteRenderer sprite;
}

public class Parallaxing : MonoBehaviour 
{
	private Background[] backgrounds;
	private float[] parallaxScale;
	private const float smoothing = 0.25f;
	private Transform cameraTransform;
	private Vector3 previousCameraPos;

	public void Awake() 
	{
		cameraTransform = Camera.main.transform;

        InstantiateDuplicates();

		parallaxScale = new float[transform.childCount];
		backgrounds = new Background[transform.childCount];

		int i = 0;
		foreach (Transform t in transform) 
        {
			backgrounds[i].transform = t;
            backgrounds[i].sprite = backgrounds[i].transform.gameObject.GetComponent<SpriteRenderer>();
			parallaxScale[i] = t.position.z * -1;
            i++;
		}
         
		previousCameraPos = cameraTransform.position;
	}

	public void Update() 
	{
		for (int i = 0; i < backgrounds.Length; i++) 
		{
			var parallax = (previousCameraPos.x - cameraTransform.position.x) * parallaxScale [i];
			var backgroundTargetPosX = backgrounds[i].transform.position.x + parallax;

			var backgroundTargetPos = new Vector3(backgroundTargetPosX,
				                          		   backgrounds [i].transform.position.y, 
				                          		   backgrounds [i].transform.position.z);
			
			backgrounds[i].transform.position = Vector3.Lerp (backgrounds [i].transform.position,
													 backgroundTargetPos,
													 smoothing * Time.deltaTime);	
		}
            
		previousCameraPos = cameraTransform.position;
	}

    private void InstantiateDuplicates()
    {
        var transforms = new Transform[transform.childCount];

        int i = 0;
        foreach (Transform t in transform)
            transforms[i++] = t;

        foreach(Transform t in transforms)
        {
            Instantiate(
                t.gameObject, 
                new Vector3(t.position.x - t.GetComponent<Renderer>().bounds.size.x + 0.1f, t.position.y, t.position.z), 
                new Quaternion(), 
                gameObject.transform
            );

            Instantiate(
                t.gameObject, 
                new Vector3(t.position.x + t.GetComponent<Renderer>().bounds.size.x - 0.1f, t.position.y, t.position.z), 
                new Quaternion(), 
                gameObject.transform
            );        
        }
    }
}