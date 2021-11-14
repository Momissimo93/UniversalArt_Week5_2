using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


//The GameManager takes value from the MenuBehaviour class and update the game: mechanics, contents and functionalities
public class GameManager : MonoBehaviour
{
    public static AudioSource master;
    // Start is called before the first frame update
    void Start()
    {
        master = gameObject.GetComponent<AudioSource>();
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

    //Returns the base 20 logarithm of a specified number as the gain is in Db
    public static void SetMasterVolume(float volume)
    {
        master.outputAudioMixerGroup.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20) ;
    }

    public static void SetResolution(Resolution [] sR, int i)
    {
        Debug.Log("Size: " + sR[i].width + " x " + sR[i].height);
        Resolution resolution = sR[i];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
