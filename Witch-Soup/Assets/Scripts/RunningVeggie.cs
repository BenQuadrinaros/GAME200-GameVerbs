using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningVeggie : MonoBehaviour
{
    public Vector2 direction = new Vector2(0, 0);
    public float patrolSpeed = 5;
    public float runningSpeed = 7;
    GameObject player;
    public float triggerRange = 10;
    
    public bool patrolling = true;
    
    private Rigidbody2D veggie;
    public Transform wallCheckPointL;
    public Transform wallCheckPointR;
    public Transform wallCheckPointU;
    public Transform wallCheckPointD;
    public float circleRadius = 0.2f;
    public LayerMask wall;
    public bool isWalled;
    public bool LisWalled, RisWalled, UisWalled, DisWalled;
    // Start is called before the first frame update
    void Start()
    {
        veggie = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float playerDistance = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));
            
        Debug.Log(playerDistance);
        if (playerDistance <= triggerRange)
        {
            patrolling = false;
        }
        else {
            patrolling = true;
        }

        if (patrolling)
        {
            patrol();
        }
        else {
            running();
        }
    }

    void patrol() {
        veggie.velocity = direction * patrolSpeed ;
    }

    void running() {
        veggie.velocity = direction * runningSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        turningPoint point = collision.GetComponent<turningPoint>();

        if (patrolling) {
            if (collision.gameObject.tag == "turingPoints")
            {
                int i = Random.Range(0, point.availableDirections.Count);
                direction = point.availableDirections[i];
            }
        }else{
            Vector2 tempDirection = new Vector2(0, 0);
            float maxDistance = 0;

            foreach (Vector2 availableDirection in point.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (player.transform.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    tempDirection = availableDirection;
                    maxDistance = distance;
                }
            }
            direction = tempDirection;

        }

    }


}
