using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectableOption : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
	Button button;
	LayoutElement layoutElement;
	public float MinimumWidth = 100f;
	public float ExpandedWidth = 600f;
	public string text;
	Text shortText;
	Text longText;

	void Awake()
	{
		button = GetComponent<Button>();
		layoutElement = GetComponent<LayoutElement>();
		layoutElement.minWidth = MinimumWidth;
		shortText = GetComponentsInChildren<Text>(true)[0];
		longText = GetComponentsInChildren<Text>(true)[1];


		if (this.gameObject == EventSystem.current.currentSelectedGameObject)
		{
			button.OnSelect(null);
			layoutElement.minWidth = ExpandedWidth;
			DisplayText(true);

		}
		else DisplayText(false);

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!EventSystem.current.alreadySelecting)
			button.Select();

	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	public void OnSelect(BaseEventData eventData)
	{
		layoutElement.minWidth = ExpandedWidth;
		DisplayText(true);

	}

	public void OnDeselect(BaseEventData eventData)
	{
		button.OnPointerExit(null);
		layoutElement.minWidth = MinimumWidth;
		DisplayText(false);

	}
	public void UpdateText(string optionText)
	{
		string[] split = optionText.Split("$".ToCharArray());
		shortText.text = split[0];
		longText.text = split[0] + split[1];
	}
	void DisplayText(bool isLong)
	{
		longText.gameObject.SetActive(isLong);
		shortText.gameObject.SetActive(!isLong);
	}

}
