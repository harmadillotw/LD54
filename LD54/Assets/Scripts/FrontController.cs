using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrontController : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioSource audioSourceShort;

    public AudioClip explodeClip;

    private void Awake()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        audioSourceShort = GameObject.Find("Audio Source Short").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barricade")
        {
            GlobalValues.speed -= 5;

            if (GlobalValues.speed < 0)
            {
                GlobalValues.speed = 0;
            }
            Utils.playAudio(explodeClip, audioSourceShort, false);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Destination")
        {
            SceneManager.LoadScene("StationScene");
        }
    }
}
