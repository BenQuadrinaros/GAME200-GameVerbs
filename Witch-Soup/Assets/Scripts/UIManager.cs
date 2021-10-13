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
    public Button bu_quit;

    // Start is called before the first frame update
    void Start()
    {
        bu_pause.onClick.AddListener( delegate{ PauseGame(); });
        bu_resume.onClick.AddListener( delegate{ ResumeGame(); });
        bu_quit.onClick.AddListener( delegate{ SceneManager.LoadSceneAsync("Menu"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PauseGame() {
        Time.timeScale = 0;
        panel_pause.SetActive(true);
    }

    void ResumeGame() {
        Time.timeScale = 1;
        panel_pause.SetActive(false);
    }
}
