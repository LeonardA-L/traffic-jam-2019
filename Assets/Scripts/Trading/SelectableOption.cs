﻿using System.Collections;
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

	void Start()
	{
		button = GetComponent<Button>();
		layoutElement = GetComponent<LayoutElement>();
		layoutElement.minWidth = MinimumWidth;
		if (this.gameObject == EventSystem.current.currentSelectedGameObject)
		{
			button.OnSelect(null);
			layoutElement.minWidth = ExpandedWidth;

		}
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

	}

	public void OnDeselect(BaseEventData eventData)
	{
		button.OnPointerExit(null);

		layoutElement.minWidth = MinimumWidth;
	}
}