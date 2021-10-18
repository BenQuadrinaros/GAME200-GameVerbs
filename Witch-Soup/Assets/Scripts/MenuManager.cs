using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button bu_play;
    public Button bu_credits;
    public Button bu_quit;

    // Start is called before the first frame update
    void Start()
    {
        //Create listener events for UI
        bu_play.onClick.AddListener( delegate{ StartGame(); });
        bu_credits.onClick.AddListener( delegate{ ShowCredits(); });
        bu_quit.onClick.AddListener( delegate{ Application.Quit(); });


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame() {
        //If we want to save data between games, we need to change this line
        PlayerPrefs.DeleteAll();

        SceneManager.LoadSceneAsync("Level 0");
    }

    void ShowCredits() {
        Debug.Log("Empty, for now.");
    }
}
