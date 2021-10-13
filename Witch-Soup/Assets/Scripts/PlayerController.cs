using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up") || Input.GetKey("w")) {
            transform.localPosition += Time.timeScale * 0.2f * Vector3.up;
        }
        if(Input.GetKey("down") || Input.GetKey("s")) {
            transform.localPosition -= Time.timeScale * 0.2f * Vector3.up;
        }
        if(Input.GetKey("left") || Input.GetKey("a")) {
            transform.localPosition -= Time.timeScale * 0.2f * Vector3.right;
        }
        if(Input.GetKey("right") || Input.GetKey("d")) {
            transform.localPosition += Time.timeScale * 0.2f * Vector3.right;
        }
    }
}
