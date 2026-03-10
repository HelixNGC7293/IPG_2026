using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

[System.Serializable]
public class DialogueNode
{
	[TextArea(3, 10)]
	public string dialogueText; // The text the NPC says
	public bool continueToNext; // The text the NPC says
	public List<DialogueOption> options; // List of choices for the player
}

[System.Serializable]
public class DialogueOption
{
	public string optionText; // What the button says
	public int nextNodeIndex; // The index of the node this option leads to
}

public class DialogueManager : MonoBehaviour
{
	[Header("UI References")]
	public TextMeshProUGUI dialogueTextDisplay;
	public Transform optionsParent;
	public GameObject optionButtonPrefab;

	[Header("Dialogue Data")]
	public List<DialogueNode> dialogueNodes; // Your story "map"
	private int currentNodeIndex = 0;
	public TypewriterEffect typewriter;

	[Header("Input Settings")]
	private PlayerInput playerInput;
	private InputAction clickAction; // Assign this in Inspector or via Script

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		clickAction = playerInput.actions["Fire"];
		// Start the game with the first node
		DisplayNode(0);
	}


	void Update()
	{
		// New Input System check for "Was this action performed this frame?"
		if (clickAction.WasPressedThisFrame())
		{
			HandleInput();
		}
	}

	void HandleInput()
	{
		if (typewriter.IsCurrentlyTyping())
		{
			typewriter.SkipOrContinue();
		}
		else
		{
			if (dialogueNodes[currentNodeIndex].continueToNext)
			{
				OnOptionSelected(currentNodeIndex + 1);
			}
			else if (dialogueNodes[currentNodeIndex].options.Count == 0)
			{
				// If text is done, check if we should auto-advance
				// (Only if there are no branching options to click)
				Debug.Log("Dialogue Ended.");
			}
		}
	}
	public void DisplayNode(int nodeIndex)
	{
		// Update current index
		currentNodeIndex = nodeIndex;
		DialogueNode node = dialogueNodes[nodeIndex];

		// 1. Set the main dialogue text
		typewriter.StartTyping(node.dialogueText);
		//dialogueTextDisplay.text = node.dialogueText;

		// 2. Clear old option buttons
		foreach (Transform child in optionsParent)
		{
			Destroy(child.gameObject);
		}

		// 3. Create new buttons for each option
		foreach (DialogueOption option in node.options)
		{
			GameObject btnObj = Instantiate(optionButtonPrefab, optionsParent);
			btnObj.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;

			// Add click listener: when clicked, go to the linked node
			int targetNode = option.nextNodeIndex;
			btnObj.GetComponent<Button>().onClick.AddListener(() => OnOptionSelected(targetNode));
		}
	}

	void OnOptionSelected(int index)
	{
		if (index < 0)
		{
			Debug.Log("End of Dialogue");
			// You could hide the UI here
			return;
		}
		DisplayNode(index);
	}
}