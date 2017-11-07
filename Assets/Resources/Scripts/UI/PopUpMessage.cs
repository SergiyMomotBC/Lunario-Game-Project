using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpMessage : MonoBehaviour
{
    public Text titleText;
    public Text messageText;
    public EventSystem eventSystem;
    public Button okayButton;

    public bool isDisplayingMessage { get; private set; }
	
	public void PresentMessage(string title, string message)
    {
        okayButton.interactable = true;
        isDisplayingMessage = true;
        eventSystem.enabled = true;
        transform.localScale = Vector3.one;
        Time.timeScale = 0.0f;

        titleText.text = title;
        messageText.text = message;
    }

    public void OkayButtonPressed()
    {
        isDisplayingMessage = false;
        Time.timeScale = 1.0f;
        transform.localScale = Vector3.zero;
        eventSystem.enabled = false;
        okayButton.interactable = false;
    }
}
