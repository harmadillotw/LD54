using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{

    public int compId;

    public AudioSource audioSource;
    public AudioSource audioSourceShort;
    
    public AudioClip damageClip;

    private Color originalColor;
    private Color damageColor = Color.red;
    private bool hitDetected;
    private float hitTimer;
    private SpriteRenderer sr;

    private TrainComponent comp;

    private void Awake()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        audioSourceShort = GameObject.Find("Audio Source Short").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        hitDetected = false;
        hitTimer = 0f;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        comp = GlobalValues.train.trainComponents[compId];
    }

    // Update is called once per frame
    void Update()
    {
        if (comp.health > 0)
        {

            if (hitDetected)
            {
                hitTimer += Time.deltaTime;
                if (hitTimer < 1f)
                {
                    if (sr.color == originalColor)
                    {
                        sr.color = damageColor;
                    }
                    else
                    {
                        sr.color = originalColor;
                    }
                }
                else
                {
                    hitDetected = false;
                    hitTimer = 0f;
                    sr.color = originalColor;
                }
            }
        }
        else
        {
            sr.color = damageColor;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Utils.playAudio(damageClip, audioSourceShort, false);
            comp.health -= 2;
            if (comp.health < 0)
            {
                if  (GlobalValues.train.trainComponents[compId].inventory.Count > 0)
                {
                    GlobalValues.train.trainComponents[compId].inventory.Clear();
                }
            }
            hitDetected = true;
        }
    }

}
