using UnityEngine;
using UnityEngine.UI;

public class ToggleSpriteSwap : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;

    private Toggle toggle;
    private Image toggleSprite;
    private AudioSource sound;

	public void Awake()
    {
        sound = GetComponent<AudioSource>();
        toggle = GetComponent<Toggle>();
        toggleSprite = transform.FindChild("Background").GetComponent<Image>();
        toggleSprite.overrideSprite = toggle.isOn ? onSprite : offSprite;
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
	}
	
	private void OnToggleValueChanged(bool newValue)
    {
        toggleSprite.overrideSprite = newValue ? onSprite : offSprite;
        sound.Play();
    }
}
