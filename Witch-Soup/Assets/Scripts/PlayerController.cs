using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Neccessary components
    private Rigidbody2D rbody;
    public GameObject Prefab_Knife;
    private float cut_cooldown;
    private GameObject curr_knife;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true;
        cut_cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta_movement = new Vector3();

        //Keyboard movement
        if(Time.timeScale > 0) {
            if(Input.GetKey("up") || Input.GetKey("w")) {
                rbody.AddForce(new Vector2(0, Time.timeScale * 2));
                delta_movement += Vector3.up;
            }
            if(Input.GetKey("down") || Input.GetKey("s")) {
                rbody.AddForce(new Vector2(0, Time.timeScale * -2));
                delta_movement -= Vector3.up;
            }
            if(Input.GetKey("left") || Input.GetKey("a")) {
                rbody.AddForce(new Vector2(Time.timeScale * -2, 0));
                delta_movement -= Vector3.right;
            }
            if(Input.GetKey("right") || Input.GetKey("d")) {
                rbody.AddForce(new Vector2(Time.timeScale * 2, 0));
                delta_movement += Vector3.right;
            }
        }

        //Rotate in direction of movement
        if(delta_movement.magnitude > 0) { 
            transform.localRotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.up, delta_movement, Vector3.forward));
        }

        //Reduce cooldown
        if(cut_cooldown > 0) {
            if(curr_knife != null) {
                //Rotate knife
                curr_knife.transform.RotateAround(transform.position, Vector3.forward, 360*Time.deltaTime);
            }
            cut_cooldown -= Time.deltaTime;
        } else {
            if(curr_knife != null) {
                Destroy(curr_knife);
            }
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) {
                //Create knife
                curr_knife = Instantiate(Prefab_Knife, transform);
                curr_knife.transform.localScale *= 8;
                curr_knife.transform.localPosition += 15*Vector3.up;

                //Set cooldown
                cut_cooldown = 1;
            }
        }
    }
}
