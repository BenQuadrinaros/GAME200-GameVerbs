using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieController : MonoBehaviour
{
    public float remaining_pieces;
    private float invinc_timer;
    public GameObject Prefab_Fragment;
    private AudioSource SFX;
    private RunningVeggie running;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        invinc_timer = 0.75f;
        SFX = GetComponent<AudioSource>();
        running = GetComponent<RunningVeggie>();
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invinc_timer > 0) { invinc_timer -= Time.deltaTime; }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name.Contains("Knife")) {
            Debug.Log("Player cut a " + gameObject.name);
            SFX.Play();
            // UIManager.score += 5;
            if (remaining_pieces > 0 && !(invinc_timer > 0)) {
                particles.Play();
                GameObject temp = Instantiate(Prefab_Fragment);
                temp.transform.position = transform.position + new Vector3(Random.value, Random.value, 0);
                VeggieController veg = temp.GetComponent<VeggieController>();
                veg.remaining_pieces = 0;
                running.runningSpeed += 0.5f;
                veg.GetComponent<RunningVeggie>().runningSpeed = running.runningSpeed;
                --remaining_pieces;
                invinc_timer = 0.75f;
            }
        } else if (col.name == "cauldron" && remaining_pieces != 0) {
            running.direction *= -1;
        } else if (col.name == "cauldron" && remaining_pieces == 0) {
            Destroy(gameObject);
        }
    }
}
