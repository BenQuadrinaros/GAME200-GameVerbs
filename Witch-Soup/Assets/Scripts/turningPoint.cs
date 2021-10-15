using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turningPoint : MonoBehaviour
{
    public LayerMask wall;
    public List<Vector2> availableDirections { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        availableDirections = new List<Vector2>();
        findAvailableDirection(Vector2.up);
        findAvailableDirection(Vector2.down);
        findAvailableDirection(Vector2.left);
        findAvailableDirection(Vector2.right);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void findAvailableDirection(Vector2 direction) {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, direction, 2, wall);
        if (hit.collider == null) {
            availableDirections.Add(direction);
        }

    }

}
