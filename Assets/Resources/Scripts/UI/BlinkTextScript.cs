using UnityEngine;
using UnityEngine.UI;

public class BlinkTextScript : MonoBehaviour 
{
    public string sceneToTransition;

    private Text text;
    private float alpha;

	public void Awake() 
    {
        text = GetComponent<Text>();
        alpha = 1.0f;
	}
	
	public void Update() 
    {
        if (Input.anyKey)
            Transition.Instance.MakeTransitionTo(sceneToTransition);

        alpha = Mathf.PingPong(Time.time * 1.5f, 1.0f);
 
        var color = text.color;
        color.a = alpha;
        text.color = color;
	}
}