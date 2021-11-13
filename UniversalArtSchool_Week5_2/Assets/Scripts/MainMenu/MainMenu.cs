using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Dropdown m_Dropdown;
    private string[] screenRosolutions;

    // Start is called before the first frame update
    void Start()
    {
        //Options();
        SetOptions(m_Dropdown);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene("Level1");

    }
    public void LoadGame()
    {
        Debug.Log("LoadGame");
    }
    public void GameCredits()
    {
        Debug.Log("Credits");
    }
    public void SetOptions(Dropdown mD)
    {
        //Resolution[] rs = Screen.resolutions;
        screenRosolutions = new string [Screen.resolutions.Length];

        for(int i = 0; i < Screen.resolutions.Length; i ++)
        {
            screenRosolutions[i] = Screen.resolutions[i].ToString();
            //Debug.Log(screenRosolutions[i]);
        }

        List<string> m_DropOptions = new List<string>(screenRosolutions);

        for (int i = 0; i < m_DropOptions.Count; i++)
        {
            Debug.Log(m_DropOptions[i]); 
        }
        m_Dropdown.ClearOptions();
        m_Dropdown.AddOptions(m_DropOptions);
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}
