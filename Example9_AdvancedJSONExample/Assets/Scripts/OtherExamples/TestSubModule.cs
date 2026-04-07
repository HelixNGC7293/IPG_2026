using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSubModule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestManager.instance.eventTest += LoadEventHandler;
    }

	private void OnDestroy()
    {
        TestManager.instance.eventTest -= LoadEventHandler;
    }

	public void LoadEventHandler(EnemyBase enemy)
	{
        print(name + " detected load data event!");
		print("name: " + enemy.name);
		print("hp: " + enemy.hp);
		print("atk: " + enemy.atk);
		print("def: " + enemy.def);

		TestManager.instance.SaveData(enemy, "enemyData2.txt");
		TestManager.instance.eventTest -= LoadEventHandler;
	}
}
