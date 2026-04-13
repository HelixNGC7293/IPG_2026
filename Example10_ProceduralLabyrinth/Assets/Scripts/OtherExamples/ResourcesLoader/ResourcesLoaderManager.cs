using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyBase
{
    public string name;
    public int hp;
    public int atk;
    public int def;
}

public class ResourcesLoaderManager : MonoBehaviour
{
    public delegate void EventTest(EnemyBase enemy);
    public EventTest eventTest;

    public static ResourcesLoaderManager instance;

    bool isLoaded = false;
    private void Awake()
    {
		if (instance == null)
		{
            instance = this;
		}
    }

	// Start is called before the first frame update
	void Update()
    {
        if(Random.value < 0.001f && !isLoaded)
		{
			isLoaded = true;
			//Player steps into this dungeon room, read data and spawn enemy
			LoadData();
		}
    }

    public void LoadData()
	{
		//Load enemy1's data
		TextAsset txtAsset = Resources.Load<TextAsset>("enemyData/enemyData1");
        string textContent = txtAsset.text;
        print(textContent);
        string[] data = textContent.Split("##");
        EnemyBase enemy = new EnemyBase();
        enemy.name = data[0];
        enemy.hp = int.Parse(data[1]);
        enemy.atk = int.Parse(data[2]);
        enemy.def = int.Parse(data[3]);

        eventTest?.Invoke(enemy);
    }
	public void SaveData(EnemyBase enemy, string fileName)
	{
		// Convert enemy data to text
		string enemyData = enemy.name + "##" + enemy.hp + "##" + enemy.atk + "##" + enemy.def;

		// Define the path to save the file
		string path = Path.Combine(Application.dataPath, "Resources/enemyData/" + fileName);

		// Write the text to a file
		File.WriteAllText(path, enemyData);
	}
}
