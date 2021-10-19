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

    public Transform upWallCheckPoint;
    public Transform downWallCheckPoint;
    public Transform leftWallCheckPoint;
    public Transform rightWallCheckPoint;
    bool UisWalled;
    bool DisWalled;
    bool LisWalled;
    bool RisWalled;
    public float circleRadius = 0.2f;
    public LayerMask wall;
    bool isStacked = false;
    // Start is called before the first frame update
    void Start()
    {
        veggie = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        veggie.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        checkWalled();
        if (isStacked)
        {
            backToTrack();
        }
        else {
            runningStates();
        }

        

        

    }

    void patrol()
    {
        veggie.velocity = direction * patrolSpeed;
    }

    void running()
    {
        veggie.velocity = direction * runningSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "turingPoints") {
            isStacked = false;
            turningPoint point = collision.GetComponent<turningPoint>();

            if (patrolling)
            {
            
                int i = Random.Range(0, point.availableDirections.Count);
                direction = point.availableDirections[i];

            }
            else
            {
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

    private void runningStates() {
        float playerDistance = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));

        if (playerDistance <= triggerRange)
        {
            patrolling = false;
        }
        else
        {
            patrolling = true;
        }

        if (patrolling)
        {
            patrol();
        }
        else
        {
            running();
        }
    }

    private void checkWalled() {
        LisWalled = Physics2D.OverlapCircle(leftWallCheckPoint.position, circleRadius, wall);
        RisWalled = Physics2D.OverlapCircle(rightWallCheckPoint.position, circleRadius, wall);
        UisWalled = Physics2D.OverlapCircle(upWallCheckPoint.position, circleRadius, wall);
        DisWalled = Physics2D.OverlapCircle(downWallCheckPoint.position, circleRadius, wall);

        if (LisWalled && direction.x == -1) {
            isStacked = true;
        } else if (RisWalled && direction.x == 1) {
            isStacked = true;
        } else if (UisWalled && direction.y == 1) {
            isStacked = true;
        }else if (DisWalled && direction.y == -1) {
            isStacked = true;
        }
    
    
    }

    private void backToTrack() {
        
        float distanceToClosestPoint= Mathf.Infinity;
        GameObject closestPoint = null;

        GameObject[] turningPoints = GameObject.FindGameObjectsWithTag("turingPoints");
        
        foreach (GameObject point in turningPoints)
        {
            
            float distance = (point.transform.position - transform.position).sqrMagnitude;

            if (distance < distanceToClosestPoint)
            {
                distanceToClosestPoint = distance;
                closestPoint = point;
            }
        }

        veggie.position = Vector2.MoveTowards(transform.position, closestPoint.transform.position, runningSpeed * Time.deltaTime); ;
        
        if (veggie.position.x == closestPoint.transform.position.x && veggie.position.y == closestPoint.transform.position.y) {
            isStacked = false; 
        }


    }




}