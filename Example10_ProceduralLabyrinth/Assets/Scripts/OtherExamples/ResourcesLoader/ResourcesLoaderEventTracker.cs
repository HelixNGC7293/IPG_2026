using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourcesLoaderEventTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		ResourcesLoaderManager.instance.eventTest += LoadEventHandler;
    }

	private void OnDestroy()
    {
		ResourcesLoaderManager.instance.eventTest -= LoadEventHandler;
    }

	public void LoadEventHandler(EnemyBase enemy)
	{
        print(name + " detected load data event!");
		print("name: " + enemy.name);
		print("hp: " + enemy.hp);
		print("atk: " + enemy.atk);
		print("def: " + enemy.def);

		ResourcesLoaderManager.instance.SaveData(enemy, "enemyData2.txt");
		ResourcesLoaderManager.instance.eventTest -= LoadEventHandler;
	}
}
