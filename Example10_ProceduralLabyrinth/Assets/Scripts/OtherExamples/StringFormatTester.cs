using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StringFormatTester : MonoBehaviour
{
    string npcName = "Eddy";
    string playerName = "Bob";

	//***Example - Stack
	private Stack<string> history = new Stack<string>();

	// Start is called before the first frame update
	void Start()
    {
        //Use string.Format to replace strings
        string original1 = "Hello {0}, this is {1}";
		print(string.Format(original1, npcName, playerName));

        //Without using string.Format, directly read variables
		string original2 = $"Hello {npcName}, this is {playerName}";
		print(original2);



		//***Example - Stack
		history.Push("Page 1");
		history.Push("Page 2");
		history.Push("Page 3");
		string previousPage = history.Pop();
		Debug.Log("Going back to: " + previousPage);
		Debug.Log("Remain Pages: " + history.Count);
	}
}
