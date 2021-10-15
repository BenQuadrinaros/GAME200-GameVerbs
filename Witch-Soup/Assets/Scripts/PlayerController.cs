using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Neccessary components
    private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Keyboard movement
        if(Input.GetKey("up") || Input.GetKey("w")) {
            rbody.AddForce(new Vector2(0, Time.timeScale * 2));
            //transform.localPosition += Time.timeScale * 0.2f * Vector3.up;
        }
        if(Input.GetKey("down") || Input.GetKey("s")) {
            rbody.AddForce(new Vector2(0, Time.timeScale * -2));
            //transform.localPosition -= Time.timeScale * 0.2f * Vector3.up;
        }
        if(Input.GetKey("left") || Input.GetKey("a")) {
            rbody.AddForce(new Vector2(Time.timeScale * -2, 0));
            //transform.localPosition -= Time.timeScale * 0.2f * Vector3.right;
        }
        if(Input.GetKey("right") || Input.GetKey("d")) {
            rbody.AddForce(new Vector2(Time.timeScale * 2, 0));
            //transform.localPosition += Time.timeScale * 0.2f * Vector3.right;
        }

        //Calculate collisions

    }
}
