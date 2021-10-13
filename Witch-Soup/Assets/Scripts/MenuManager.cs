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
        bu_play.onClick.AddListener( delegate{ SceneManager.LoadSceneAsync("Level1"); });
        bu_credits.onClick.AddListener( delegate{ ShowCredits(); });
        bu_quit.onClick.AddListener( delegate{ Application.Quit(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowCredits() {
        Debug.Log("Empty, for now.");
    }
}
