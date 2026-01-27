using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
	RectTransform rectTrans;
	bool isDragging = false;

	// Start is called before the first frame update
	void Start()
	{
		rectTrans = GetComponent<RectTransform>();
	}

	//Drag Detection
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (!isDragging)
			{
				isDragging = true;
			}
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (isDragging)
			{
				rectTrans.anchoredPosition += eventData.delta;

				//Another way 1: control anchoredPosition Vector2 to make it follow the mouse
				//rectTrans.anchoredPosition = eventData.position - new Vector2(Screen.width / 2, Screen.height / 2);

				//Another way 2: convert global position to move the card
				//Vector3 globalMousePos;
				//if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTrans, eventData.position, eventData.pressEventCamera, out globalMousePos))
				//{
				//	rectTrans.position = globalMousePos;
				//}


			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (isDragging)
			{
				//End Drag
				isDragging = false;
			}
		}
	}
}
