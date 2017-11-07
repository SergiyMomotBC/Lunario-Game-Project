using UnityEngine;
using UnityEngine.UI;

public class ButtonPressedEffects : MonoBehaviour
{
    private Button button;
    private AudioSource pressAudio;

	public void Awake()
    {
        pressAudio = gameObject.AddComponent<AudioSource>();
        pressAudio.clip = Resources.Load<AudioClip>("Sounds/button_pressed");
        pressAudio.playOnAwake = false;

        button = GetComponent<Button>();

        if(pressAudio != null)
            button.onClick.AddListener(() => { pressAudio.Play(); });

        button.onClick.AddListener(() =>
        {
            GetComponent<SelectedButton>().enabled = false;
            var scale = transform.localScale;
            scale.x = transform.localScale.x * 0.9f;
            scale.y = transform.localScale.y * 0.9f;
            transform.localScale = scale;
            Invoke("ReturnToNormal", 0.1f);
        });
	}

    public void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    private void ReturnToNormal()
    {
        GetComponent<SelectedButton>().enabled = true;
        var scale = transform.localScale;
        scale.x = transform.localScale.x / 0.9f;
        scale.y = transform.localScale.y / 0.9f;
        transform.localScale = scale;
    }
}
