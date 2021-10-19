using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Button bu_start;

    // Start is called before the first frame update
    void Start()
    {
        bu_start.onClick.AddListener( delegate{ StartGame(); });
        Time.timeScale = 0;
    }

    void StartGame() {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
