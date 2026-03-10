using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
	public TMP_Text textComponent;
	public float timeBetweenChars = 0.05f;

	private Coroutine typingCoroutine;
	private bool isTyping = false;
	private int totalVisibleCharacters;

	public void StartTyping(string fullText)
	{
		if (typingCoroutine != null)
		{
			StopCoroutine(typingCoroutine);
		}

		typingCoroutine = StartCoroutine(TypeText(fullText));
	}

	private IEnumerator TypeText(string textToType)
	{
		isTyping = true;
		textComponent.text = textToType;
		textComponent.maxVisibleCharacters = 0;

		yield return null; // Wait for TMP to initialize mesh

		totalVisibleCharacters = textToType.Length;
		int counter = 0;

		while (counter <= totalVisibleCharacters)
		{
			textComponent.maxVisibleCharacters = counter;
			counter++;
			yield return new WaitForSeconds(timeBetweenChars);
		}

		isTyping = false;
		typingCoroutine = null;
	}

	// This is the key method for "Skip" functionality
	public void SkipOrContinue()
	{
		if (isTyping)
		{
			// 1. If currently typing, stop the coroutine and show everything
			StopCoroutine(typingCoroutine);
			textComponent.maxVisibleCharacters = totalVisibleCharacters;
			isTyping = false;
			typingCoroutine = null;
		}
		else
		{
			// 2. If already finished, logic for "Next Page" would go here
			Debug.Log("Already finished, ready for next step.");
		}
	}

	public bool IsCurrentlyTyping() => isTyping;
}