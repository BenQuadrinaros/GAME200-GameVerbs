using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingMinigame : MonoBehaviour
{
    private bool active;
    private List<string> veggie_list;
    public Text text_score;
    private float launch_timer;
    public GameObject Prefab_CutVeggie;

    //Different veggie textures
    public Sprite texture_cabbage;
    public Sprite texture_mushroom;
    public Sprite texture_tomato;
    public Sprite texture_pumpkin;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        veggie_list = new List<string>();
        veggie_list.Add("cabbage");
        veggie_list.Add("mushroom");
        veggie_list.Add("tomato");
        veggie_list.Add("pumpkin");
        launch_timer = 3 + Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        //Update veggie launcher
        if(active) {
            Debug.Log(this+" is active");
            if(PlayerPrefs.HasKey(veggie_list[0])) {
                transform.localPosition += new Vector3(Time.deltaTime*3*Mathf.Sin(Time.time), 0, 0);
                launch_timer -= Time.deltaTime;
                if(launch_timer < 0) {
                    //Create and fling the correct veggie
                    GameObject veggie = Instantiate(Prefab_CutVeggie, transform);
                    if(veggie_list[0] == "cabbage") {
                        veggie.GetComponent<SpriteRenderer>().sprite = texture_cabbage;
                    } else if(veggie_list[0] == "mushroom") {
                        veggie.GetComponent<SpriteRenderer>().sprite = texture_mushroom;
                    } else if(veggie_list[0] == "tomato") {
                        veggie.GetComponent<SpriteRenderer>().sprite = texture_tomato;
                    } else if(veggie_list[0] == "pumpkin") {
                        veggie.GetComponent<SpriteRenderer>().sprite = texture_pumpkin;
                    }
                    veggie.GetComponent<Rigidbody2D>().AddForce(new Vector2(17, 100));

                    //Decrement the veggie count for that one
                    Debug.Log("Decrementing "+veggie_list[0]);
                    int veggie_count = PlayerPrefs.GetInt(veggie_list[0]);
                    --veggie_count;
                    if(veggie_count == 0) {
                        PlayerPrefs.DeleteKey(veggie_list[0]);
                    } else {
                        PlayerPrefs.SetInt(veggie_list[0], veggie_count);
                    }

                    //Reset timer
                    launch_timer = 0.75f + Random.value;
                }
            } else {
                Debug.Log("Removing "+veggie_list[0]);
                veggie_list.RemoveAt(0);
                if(veggie_list.Count == 0) {
                    active = false;
                }
            }
        }

        //Detect mouse clicks
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null) {
                hit.collider.gameObject.GetComponent<VeggieLaunched>().getClicked();
            }
        }

        //Update score
        text_score.text = "Soup Score: "+UIManager.score;
        Debug.Log("score becomes "+UIManager.score);
    }
}
