using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Interface Buttons
    public Button bu_pause;
    public Button bu_resume;
    public Button bu_next;
    public Button bu_cook;
    public Button bu_quit;

    //Interface elements
    public GameObject panel_pause;
    public Text te_cook;
    public Scrollbar sc_hunger;
    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        //Add listener events
        bu_pause.onClick.AddListener( delegate{ PauseGame(); });
        bu_next.onClick.AddListener( delegate{ NextLevel(); });
        bu_cook.onClick.AddListener( delegate{ MoveTo("Cooking"); });
        bu_resume.onClick.AddListener( delegate{ ResumeGame(); });
        bu_quit.onClick.AddListener( delegate{ MoveTo("Menu"); });

        //Find hunger bar starting value
        if(PlayerPrefs.HasKey("HungerLevel")) {
            sc_hunger.size = PlayerPrefs.GetFloat("HungerLevel");
            PlayerPrefs.DeleteKey("HungerLevel");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(sc_hunger != null) {
            sc_hunger.size += Time.deltaTime / 120;
            if(sc_hunger.size >= 1) {
                bu_cook.gameObject.SetActive(true);
                bu_next.gameObject.SetActive(false);
                te_cook.text = "Too hungry to keep chasing!";
                PauseGame();
            }
        }
    }

    void PauseGame() {
        Time.timeScale = 0;
        panel_pause.SetActive(true);
        Cursor.visible = true;
        if(sc_hunger.size < 1 && UIManager.score > 0) {
            te_cook.text = "Veggies Caught: "+UIManager.score;
        }
    }

    void ResumeGame() {
        if(sc_hunger.size < 1) {
            Time.timeScale = 1;
            panel_pause.SetActive(false);
        } else {
            Debug.Log("I have to go make soup!");
        }
    }

    void NextLevel() {
        //Set global variables
        PlayerPrefs.SetFloat("HungerLevel", sc_hunger.size / Mathf.Max(1, UIManager.score));
        if(PlayerPrefs.HasKey("VeggiesCaught")) {
            PlayerPrefs.SetInt("VeggiesCaught", PlayerPrefs.GetInt("VeggiesCaught") + UIManager.score);
        }
        UIManager.score = 0;

        //Move to next scene
        Time.timeScale = 1;
        string level_name = SceneManager.GetActiveScene().name;
        if(level_name == "Level 0") {
            SceneManager.LoadSceneAsync("Level 1");
        } else if(level_name == "Level 1") {
            SceneManager.LoadSceneAsync("Level 2");
        } else if(level_name == "Level 2") {
            SceneManager.LoadSceneAsync("Level 3");
        } else if(level_name == "Level 3") {
            SceneManager.LoadSceneAsync("Menu");
        }
    }

    void MoveTo(string level_name) {
        //Set global variables
        if(PlayerPrefs.HasKey("VeggiesCaught")) {
            PlayerPrefs.SetInt("VeggiesCaught", PlayerPrefs.GetInt("VeggiesCaught") + UIManager.score);
        }
        UIManager.score = 0;

        //Move to next scene
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(level_name);
    }
}
