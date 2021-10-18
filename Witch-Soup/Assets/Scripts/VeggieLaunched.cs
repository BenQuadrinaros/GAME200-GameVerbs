using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieLaunched : MonoBehaviour
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
        if(col.name == "Bottom_Boundary") {
            Debug.Log(gameObject.name+" fell through");
            Destroy(gameObject);
        }
    }

    public void getClicked() {
        UIManager.score += 10 + (int)Mathf.Floor(transform.localPosition.y);
        Destroy(gameObject);
    }
}
