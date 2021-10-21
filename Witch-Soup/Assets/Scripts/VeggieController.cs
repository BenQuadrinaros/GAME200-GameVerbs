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
    public Material ma_star;

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
            SFX.Play();
            ParticleSystem.MainModule particles_main = particles.main;
            if (remaining_pieces > 0 && !(invinc_timer > 0)) {
                particles.Play();
                transform.localScale *= 0.9f;
                GameObject temp = Instantiate(Prefab_Fragment);
                --remaining_pieces;
                temp.transform.position = transform.position + new Vector3(Random.value, Random.value, 0);
                temp.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                VeggieController veg = temp.GetComponent<VeggieController>();
                veg.remaining_pieces = 0;
                temp.GetComponent<ParticleSystemRenderer>().material = ma_star;
                ParticleSystem parts = temp.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule parts_main = parts.main;
                parts_main.loop = true;
                parts.Play();
                ++running.runningSpeed;
                temp.GetComponent<RunningVeggie>().runningSpeed = running.runningSpeed + remaining_pieces;
                invinc_timer = 0.75f;
            } else if(remaining_pieces == 0 && !particles_main.loop) {
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                GetComponent<ParticleSystemRenderer>().material = ma_star;
                particles_main.loop = true;
                particles.Play();
            }
        } else if (col.name == "cauldron" && remaining_pieces != 0) {
            running.direction *= -1;
        } else if (col.name == "cauldron" && remaining_pieces == 0) {
            UIManager.score++;
            col.gameObject.GetComponent<AudioSource>().Play();
            col.gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }
}
