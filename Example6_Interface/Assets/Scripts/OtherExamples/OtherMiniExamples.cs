using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class OtherMiniExamples : MonoBehaviour
{
    
    //These are separate examples for class, unrelated to the card game example

    //***Example C - Dictionary
    Dictionary<string, int> dict = new Dictionary<string, int>();

    //***Example D - Dictionary For Loop
    Dictionary<string, int> leaderboard = new Dictionary<string, int>()
    {
        { "Player 1", 100 },
        { "Player 2", 200 },
        { "Player 3", 300 }
    };

    //***Example E - Query
    private List<int> numbers = new List<int>() { 100, 200, 300, 400, 500 };

    //***Example F - Stack & Queue
    private Stack<string> history = new Stack<string>();
	private Queue<string> history2 = new Queue<string>();

	//***Example G - Bubble Sort
	private int[] sortNumbers = { 5, 4, 1, 3, 2 };

    private void Start()
    {
        //***Example C
        dict.Add("Holy Sword Attack Damage", 98);
        dict.Add("Fireball Attack Damage", 98);
        print("Current Damage of Fireball: " + dict["Fireball Attack Damage"]);

        //***Example D
        foreach (KeyValuePair<string, int> playerScore in leaderboard)
        {
            Debug.Log("Current score of " + playerScore.Key + ": " + playerScore.Value);
        }

        //***Example E
        IEnumerable<int> highNumbers = from num in numbers
                                                where num >= 270
                                                orderby num descending // optional
                                                select num;

        foreach (int highNum in highNumbers)
        {
            Debug.Log("Current high number is " + highNum);
        }

        //***Example F
        //First in last out
        history.Push("Page 1");
        history.Push("Page 2");
        history.Push("Page 3");
        string previousPage = history.Pop();
        Debug.Log("[Stack] Going back to: " + previousPage);
        Debug.Log("[Stack] Remain Pages: " + history.Count);

		//First in first out
		history2.Enqueue("Page 1");
		history2.Enqueue("Page 2");
		history2.Enqueue("Page 3");
		string previousPage2 = history2.Dequeue();
		Debug.Log("[Queue] Going back to: " + previousPage2);
		Debug.Log("[Queue] Remain Pages: " + history2.Count);

		//***Example G
		for (int i = 0; i < sortNumbers.Length - 1; i++)
        {
            for (int j = 0; j < sortNumbers.Length - i - 1; j++)
            {
                if (sortNumbers[j] > sortNumbers[j + 1])
                {
                    int temp = sortNumbers[j];
                    sortNumbers[j] = sortNumbers[j + 1];
                    sortNumbers[j + 1] = temp;
                }
            }
        }

        Debug.Log("Bubble Sort result is: \n");

		foreach (int sortNum in sortNumbers)
		{
			Debug.Log(sortNum);
		}
	}

}
