using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
	//Card UIs
	//Card Image UI Displayer
	[SerializeField]
	Image cardArt;
	//Card Name UI Displayer
	[SerializeField]
	TextMeshProUGUI text_CardName;
	//Card Basic Cost UI Displayer
	[SerializeField]
	TextMeshProUGUI text_Cost;
	//Card 2D transform (RectTransform)
	[HideInInspector]
	public RectTransform rectTrans;

	//Card Manager
	private CardManager cardManager;

	//Hovering and Selecting Scale / Position changes
	[SerializeField]
	Vector3 hoveringScale = new Vector3(2, 2, 2);
	//When mouse hovering over, change of card y to this vertical position
	[SerializeField]
	float hoveringY = 200;
	//Scale during dragging
	[SerializeField]
	Vector3 selectedScale = new Vector3(1.2f, 1.2f, 1.2f);
	//Card moving speed
	[SerializeField]
	float lerpSpeed = 3;
	//Anchor point for card position
	[HideInInspector]
	public Vector2 targetPosition = Vector2.zero;
	//Anchor rotation
	[HideInInspector]
	public Quaternion targetRotation = new Quaternion();

	//Card Status
	bool mouseRollOver = false;
	//Mouse can select the card or not
	bool allowSelect = false;
	//Mouse is dragging
	bool isDragging = false;

	//Important card info
	[HideInInspector]
	public CardProperty cardProperty;

	void Start () {
		//Wait until 1 sec after, start dragging
		//Because there is a shuffle cards animation playing at the beginning
		rectTrans = GetComponent<RectTransform>();
		rectTrans.localScale = Vector3.one;
		Invoke("AllowSelect", 1);
	}

	public void Init(CardProperty cP, CardManager cM)
	{
		//Setup card UIs
		cardManager = cM;
		cardProperty = cP;
		cardArt.sprite = cP.art;
		text_CardName.text = cP.name;
		text_Cost.text = cP.cost.ToString();
	}

	void Update()
	{
		if ((!mouseRollOver && !isDragging) || (!allowSelect && mouseRollOver) || CardManager.gameStatus != GameStatus.Ready)
		{
			//1.Nothing happens || 2.Card moving back but mouse roll over || 3.Not in player phase
			//1.Lerp movement. Move the card back to the hands (screen bottom)
			//2.Lerp movement. Move the card back to the hands (screen bottom)
			//3.Round End / Start, all the cards are moving to / from the corner
			rectTrans.anchoredPosition = Vector2.Lerp(rectTrans.anchoredPosition, targetPosition, Time.deltaTime * lerpSpeed);
			rectTrans.rotation = Quaternion.Slerp(rectTrans.rotation, rectTrans.parent.rotation * targetRotation, Time.deltaTime * lerpSpeed);
		}
		else if (allowSelect && mouseRollOver && !isDragging)
		{
			//Mouse Roll Over, change card Y to fixed position & make it bigger
			rectTrans.localScale = hoveringScale;
			rectTrans.anchoredPosition = new Vector2(targetPosition.x, hoveringY);
			//Keep it straight
			rectTrans.localRotation = Quaternion.Euler(Vector3.zero);
		}
	}

	//Mouse Roll Over Starts
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (cardManager.currentSelectedCard == null)
		{
			mouseRollOver = true;

			//All the other cards, move to the side left or right to avoid this card
			cardManager.RelocateAllCards(this);
		}
	}

	//Mouse Roll Over Ends
	public void OnPointerExit(PointerEventData eventData)
	{
		mouseRollOver = false;
		rectTrans.localScale = Vector3.one;

		//All the other cards, move back to the hand positions
		cardManager.RelocateAllCards();
	}

	//Drag Detection
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (allowSelect && eventData.button == PointerEventData.InputButton.Left)
		{
			if (!isDragging)
			{
				isDragging = true;
				//This card is selected by the player
				cardManager.currentSelectedCard = this;
			}
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (allowSelect && eventData.button == PointerEventData.InputButton.Left)
		{
			if (isDragging)
			{
				//Calculate offset so the player can smoothly pick up the card
				rectTrans.localScale = selectedScale;
				//Vector3 globalMousePos;
				//if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTrans, eventData.position, eventData.pressEventCamera, out globalMousePos))
				//{
				//	rectTrans.position = globalMousePos;
				//}

				rectTrans.anchoredPosition += eventData.delta;
				//Another way: control anchoredPosition Vector2 to make it follow the mouse (Not recommended)
				//rectTrans.anchoredPosition = eventData.position - new Vector2(Screen.width / 2, 0);
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (allowSelect && eventData.button == PointerEventData.InputButton.Left)
		{
			if (isDragging)
			{
				//End Drag
				isDragging = false;
				//Check if player drag the card to the right area
				if (CardManager.gameStatus == GameStatus.Ready)
				{
					//Taking effects
					if (cardManager.TakingEffectCheck())
					{
						//gameObject.SetActive(false);
					}
				}

				rectTrans.localScale = Vector3.one;
				allowSelect = false;
				//Cool down time before player can select the card again
				Invoke("AllowSelect", 0.5f);
				cardManager.currentSelectedCard = null;
			}
		}
	}

	//Waiting for the cards movement stop
	void AllowSelect()
	{
		allowSelect = true;
	}
}
