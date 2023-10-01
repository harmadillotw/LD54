using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispController : MonoBehaviour
{

    public GameObject projectile;
    public float fireDelta = 1F;

    public AudioSource audioSource;
    public AudioSource audioSourceShort;

    public AudioClip explodeClip;
    public AudioClip fireClip;

    private GameObject newProjectile;
    private float nextFire = 1F;
    private float myTime = 0.0F;

    private float projectileSpeed = 5F;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float idleSpeed = 0.8F;
    private float attackSpeed = 2F;
    private int state;

    private float nextRotate = 3F;
    private float idleTimer = 0.0F;

    private GameObject target;
    private Color originalColor;
    private Color damageColor = Color.red;
    private int health;
    private bool hitDetected;
    private float hitTimer;

    private void Awake()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        audioSourceShort = GameObject.Find("Audio Source Short").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        state = 1;
        originalColor = sr.color;
        health = 1;
        hitDetected = false;
        hitTimer = 0f;

        int targetNum = Random.Range(0, GlobalValues.train.trainGameObjects.Count);
        target = GlobalValues.train.trainGameObjects[targetNum];
        //transform.Rotate(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if(hitDetected)
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

        if (Vector3.Distance(target.transform.position, transform.position) > 1f)
        {
            attackSpeed = 3f;
        }
        if (Vector3.Distance(target.transform.position, transform.position) > 20f)
        {
            attackSpeed = 8f;
        }
        
        if (Vector3.Distance(target.transform.position, transform.position) < 1f)
        {
            rb.velocity = transform.right * 0.2f;
        }


        //if (state == 1)
        //{
        //    if (Vector3.Distance(target.transform.position, transform.position)< 10f)
        //    {
        //        state = 2;
        //    }
        //    idleTimer += Time.deltaTime;
        //    if (idleTimer > nextRotate)
        //    {
        //        idleTimer = 0f;
        //        float rot = Random.Range(-15f, 15f);
        //        transform.Rotate(0, 0, rot);

        //    }
        //    rb.velocity = transform.right * idleSpeed;

        //}
        
        myTime = myTime + Time.deltaTime;
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (Vector3.Distance(target.transform.position, transform.position) < 1f)
        {
            rb.velocity = transform.right * 0.2f;
        }
        else
        {                
            rb.velocity = transform.right * attackSpeed;
        }
        if (Vector3.Distance(target.transform.position, transform.position) < 10f)
        {
            if (myTime > nextFire)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < 7f)
                {
                    Utils.playAudio(fireClip, audioSourceShort, false);
                    Vector2 direction = new Vector2(
                    target.transform.position.x - transform.position.x,
                    target.transform.position.y - transform.position.y);
                    nextFire = myTime + fireDelta;
                    newProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
                    newProjectile.transform.position = transform.Find("WFireSource").position;
                    newProjectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg);
                    Rigidbody2D pRB = newProjectile.GetComponent<Rigidbody2D>();
                    pRB.angularVelocity = 0f;
                    pRB.velocity = (direction * projectileSpeed);

                    nextFire = nextFire - myTime;
                    myTime = 0.0F;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            Utils.playAudio(explodeClip, audioSourceShort, false);
            Destroy(collision.gameObject,1f);
            hitDetected = true;
            health -= 2;
        }
    }
}

