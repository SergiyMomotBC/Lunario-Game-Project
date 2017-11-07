using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private RectTransform button;
    private float scale;
    private short direction;
    private float baseScale;
    private AudioSource sound;

    public void Start()
    {
        button = GetComponent<RectTransform>();
        sound = GetComponent<AudioSource>();
        baseScale = button.localScale.x;
    }

    public void Update()
    {
        if (direction == 0)
            return;

        button.localScale = new Vector3(scale, scale, 1.0f);

        scale += direction * 0.0075f;

        if (direction > 0 && scale >= baseScale + 0.1f)
            direction = -1;
        else if (direction < 0 && scale <= baseScale - 0.1f)
            direction = 1;
    }

    public void OnSelect(BaseEventData data)
    {
        scale = baseScale - 0.1f;
        direction = 1;
        sound.Play();
    }
	
    public void OnDeselect(BaseEventData data)
    {
        direction = 0;
        button.localScale = new Vector3(baseScale, baseScale, 1.0f);
    }
}
