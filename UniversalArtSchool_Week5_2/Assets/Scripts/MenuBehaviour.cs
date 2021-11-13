using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    bool isSomethingSaved;
    [SerializeField] private GameObject noSavedGameDialog = null;
    // Start is called before the first frame update
    void Start()
    {
        isSomethingSaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameYes()
    {
        GameManager.LoadScene("Level1");
    }

    public void LoadGameYes()
    {
        if (isSomethingSaved)
        {
            //GameManager.LoadScene("SceneToLoad");
        }
        else
        {
            Debug.Log("Nothing has been saved");
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton ()
    {
        GameManager.ExitGame();
    }
}
