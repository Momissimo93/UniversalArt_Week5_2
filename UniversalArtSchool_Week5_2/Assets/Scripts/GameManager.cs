using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene("Level1");
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
}
