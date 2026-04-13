using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
	{
		Dictionary<string, int> dict = new Dictionary<string, int>();
		dict.Add("Holy Sword Attack Damage", 98);
		dict.Add("Fireball Attack Damage", 98);
		print("Current Damage of Fireball: " + dict["Fireball Attack Damage"]);


		Queue<int> queue = new Queue<int>();

		queue.Enqueue(137);
		queue.Enqueue(233);
		queue.Enqueue(998);

		print(queue.Dequeue());
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
