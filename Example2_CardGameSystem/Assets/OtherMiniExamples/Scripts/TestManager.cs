using UnityEngine;

public class TestManager : MonoBehaviour
{
    float playerScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Player finish a game
        //playerScore = 9898;
        //Save();
        print(playerScore);
        Load();

        print(playerScore);
    }

    void Save()
	{
		PlayerPrefs.SetFloat("Score", playerScore);
	}

	private void Load()
	{
        playerScore = PlayerPrefs.GetFloat("Score", 0);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
