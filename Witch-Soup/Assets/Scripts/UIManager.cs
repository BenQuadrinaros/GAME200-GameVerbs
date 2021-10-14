using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button bu_pause;
    public GameObject panel_pause;
    public Button bu_resume;
    public Button bu_cook;
    public Button bu_quit;

    public Scrollbar sc_hunger;
    public static float score;

    // Start is called before the first frame update
    void Start()
    {
        bu_pause.onClick.AddListener( delegate{ PauseGame(); });
        if(bu_cook != null) { bu_cook.onClick.AddListener( delegate{ Time.timeScale = 1; SceneManager.LoadSceneAsync("Cooking"); }); }
        bu_resume.onClick.AddListener( delegate{ ResumeGame(); });
        bu_quit.onClick.AddListener( delegate{ Time.timeScale = 1; SceneManager.LoadSceneAsync("Menu"); });
    }

    // Update is called once per frame
    void Update()
    {
        if(sc_hunger != null) {
            sc_hunger.size += Time.deltaTime / 120;
            if(sc_hunger.size >= 1) {
                Debug.Log("Too hungry to chase!");
                PauseGame();
            }
        }
    }

    void PauseGame() {
        Time.timeScale = 0;
        panel_pause.SetActive(true);
    }

    void ResumeGame() {
        if(sc_hunger.size < 1) {
            Time.timeScale = 1;
            panel_pause.SetActive(false);
        } else {
            Debug.Log("I have to go make soup!");
        }
    }
}
