using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.name == "Player_Witch") {
            Debug.Log("Player caught a "+gameObject.name);
            UIManager.score += 10;
            if(PlayerPrefs.HasKey(gameObject.name)) {
                PlayerPrefs.SetInt(gameObject.name, PlayerPrefs.GetInt(gameObject.name)+1);
            } else {
                PlayerPrefs.SetInt(gameObject.name, 1);
            }
            Destroy(gameObject);
        }
    }
}
