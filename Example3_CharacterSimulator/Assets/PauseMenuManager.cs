using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string key = Input.inputString;
        if (key == "s")
        {
            Time.timeScale = 0;
        }
        else if(key != "")
        {
            Time.timeScale = 1;
        }
    }
}
