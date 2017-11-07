using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElementButtonController : MonoBehaviour, ISelectHandler, IDeselectHandler 
{
	public int index;
	public string elementName;
	public Color color;
	public Sprite highlightedSprite;

	private Button element;
	private int currSiblingIndex;
	private Text text;
	private Image sprite;
	private Sprite normalSprite;
    private Text descriptionField;

	public void Awake() 
	{
		element = GetComponent<Button>();
		element.interactable = Game.PlayerStats.elementsUnlockString[index] == '1';

        descriptionField = GameObject.Find("Description Text").GetComponent<Text>();

		sprite = element.GetComponent<Image>();
		sprite.color = color;

		text = element.GetComponentInChildren<Text>();
		text.text = element.interactable ? elementName: "?";

		currSiblingIndex = transform.GetSiblingIndex();
		normalSprite = sprite.sprite;
	}

	public void OnSelect(BaseEventData eventData)
	{
        if(Game.Atoms[index] != null)
            descriptionField.text = Game.Atoms[index].description;

		sprite.color = Color.white;
		text.enabled = false;

		var scale = transform.localScale;
		scale.x *= 1.5f;
		scale.y *= 1.5f;
		transform.localScale = scale;

		transform.SetSiblingIndex(118);

        sprite.sprite = highlightedSprite;
	}

	public void OnDeselect(BaseEventData eventData)
	{
        descriptionField.text = "";

		sprite.color = color;
		text.enabled = true;
		sprite.sprite = normalSprite;

		var scale = transform.localScale;
		scale.x /= 1.5f;
		scale.y /= 1.5f;
		transform.localScale = scale;

		transform.SetSiblingIndex(currSiblingIndex);
	}
}