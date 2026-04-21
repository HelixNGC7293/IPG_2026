using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StringFormatTester : MonoBehaviour
{
    string npcName = "Eddy";
    string playerName = "Bob";



	// Start is called before the first frame update
	void Start()
    {
        //Use string.Format to replace strings
        string original1 = "Hello {0}, this is {1}";
		print(string.Format(original1, npcName, playerName));

        //Without using string.Format, directly read variables
		string original2 = $"Hello {npcName}, this is {playerName}";
		print(original2);

		//***Example E - Query
		List<int> numbers = new List<int>() { 100, 200, 300, 400, 500 };
		IEnumerable<int> highNumbers = from num in numbers
									   where num >= 270
									   orderby num descending // optional
									   select num;

		foreach (int highNum in highNumbers)
		{
			Debug.Log("Current high number is " + highNum);
		}
	}
}
