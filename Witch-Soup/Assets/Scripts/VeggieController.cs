using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieController : MonoBehaviour
{
    public float speed;
    public float remaining_pieces;
    private float invinc_timer;
    public GameObject Prefab_Fragment;
    public Sprite sprite_fragment;

    // Start is called before the first frame update
    void Start()
    {
        if(speed == 0) {
            speed = 1.5f;
        }
        if(remaining_pieces == 0) {
            remaining_pieces = 2;
        }
        invinc_timer = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if(invinc_timer > 0) { invinc_timer -= Time.deltaTime; }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.name.Contains("Knife")) {
            Debug.Log("Player cut a "+gameObject.name);
            // UIManager.score += 5;
            GameObject temp = Instantiate(Prefab_Fragment);
            temp.GetComponent<SpriteRenderer>().sprite = sprite_fragment;
            temp.transform.position = transform.position + new Vector3(Random.value, Random.value, 0);
            temp.transform.localScale *= 4;

            if(remaining_pieces > 1) {
                speed += 0.5f;
                --remaining_pieces;
            } else {
                temp = Instantiate(Prefab_Fragment);
                temp.GetComponent<SpriteRenderer>().sprite = sprite_fragment;
                temp.transform.position = transform.position + new Vector3(Random.value, Random.value, 0);
                temp.transform.localScale *= 4;
                Destroy(gameObject);
            }
        }
    }
}
