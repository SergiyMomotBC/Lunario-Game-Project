using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeBarController : MonoBehaviour
{
    private Image[] bars;
    private Button minusButton;
    private Button plusButton;
    private sbyte volumeLevel;
    private PointerEventData pointer;
    private Settings settings;
    private bool allowInput;

    private const int MAX_VOLUME = 10;
    private const int MIN_VOLUME = 0;

	public void Awake()
    {
        settings = Settings.Instance;
        pointer = new PointerEventData(EventSystem.current);

        bars = new Image[MAX_VOLUME];
        for (int i = 0; i < MAX_VOLUME; i++)
            bars[i] = transform.FindChild("Volume Bar " + (i+1)).GetComponent<Image>();

        minusButton = transform.FindChild("Minus Button").GetComponent<Button>();
        minusButton.onClick.AddListener(MinusButtonPress);

        plusButton = transform.FindChild("Plus Button").GetComponent<Button>();
        plusButton.onClick.AddListener(PlusButtonPress);

        volumeLevel = (sbyte) settings.volume;
        UpdateVolume();
    }

    public void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
            return;

        var horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0 && allowInput) {
            ExecuteEvents.Execute(plusButton.gameObject, pointer, ExecuteEvents.submitHandler);
            allowInput = false;
        } else if (horizontal < 0 && allowInput) {
            ExecuteEvents.Execute(minusButton.gameObject, pointer, ExecuteEvents.submitHandler);
            allowInput = false;
        } else if (horizontal == 0)
            allowInput = true;
    }

    private void MinusButtonPress()
    {
        if (volumeLevel > MIN_VOLUME) {
            volumeLevel--;
            UpdateVolume();
        }
    }

    private void PlusButtonPress()
    {
        if (volumeLevel < MAX_VOLUME) { 
            volumeLevel++;
            UpdateVolume();
        }
    }

    private void UpdateVolume()
    {
        settings.volume = volumeLevel;

        var level = volumeLevel;
        for (int i = 0; i < MAX_VOLUME; i++)
            bars[i].gameObject.SetActive(level-- > 0 ? true : false);
    }
}