using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResolutionSelectorController : MonoBehaviour
{
    private Button leftButton;
    private Button rightButton;
    private Text resolutionLabel;
    private Resolution[] supportedResolutions;
    private int currentResolutionIndex;
    private PointerEventData pointer;
    private Settings settings;
    private bool allowInput;

	public void Awake()
    {
        settings = Settings.Instance;
        pointer = new PointerEventData(EventSystem.current);

        leftButton = transform.FindChild("Left Button").GetComponent<Button>();
        leftButton.onClick.AddListener(LeftButtonPressed);

        rightButton = transform.FindChild("Right Button").GetComponent<Button>();
        rightButton.onClick.AddListener(RightButtonPressed);

        resolutionLabel = transform.FindChild("Resolution Label").GetComponent<Text>();

        supportedResolutions = Screen.resolutions;

        int i = 0;
        foreach (var res in Screen.resolutions)
        { 
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                currentResolutionIndex = i;
            i++;
        }

        UpdateResolution();
    }
	
	public void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
            return;

        var horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0 && allowInput) {
            ExecuteEvents.Execute(rightButton.gameObject, pointer, ExecuteEvents.submitHandler);
            allowInput = false;
        } else if (horizontal < 0 && allowInput) {
            ExecuteEvents.Execute(leftButton.gameObject, pointer, ExecuteEvents.submitHandler);
            allowInput = false;
        } else if (horizontal == 0)
            allowInput = true;
    }

    private void LeftButtonPressed()
    {
        currentResolutionIndex = currentResolutionIndex > 0 ? currentResolutionIndex - 1 : supportedResolutions.Length - 1;
        UpdateResolution();
    }

    private void RightButtonPressed()
    {
        currentResolutionIndex = currentResolutionIndex < supportedResolutions.Length - 1 ? currentResolutionIndex + 1 : 0;
        UpdateResolution();
    }

    private void UpdateResolution()
    {
        settings.resolution = supportedResolutions[currentResolutionIndex];

        var res = supportedResolutions[currentResolutionIndex];
        resolutionLabel.text = res.width + "x" + res.height;
    }
}
