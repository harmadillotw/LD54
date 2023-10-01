using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject projectile;
    public float fireDelta = 0.1F;

    public int compId;

    public AudioSource audioSource;
    public AudioSource audioSourceShort;

    public AudioClip fireClip;

    private GameObject newProjectile;
    private float nextFire = 0.1F;
    private float myTime = 0.0F;

    private float projectileSpeed = 9F;
    // Start is called before the first frame update

    private void Awake()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        audioSourceShort = GameObject.Find("Audio Source Short").GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalValues.editMode)
        {
            Vector3 mouseScreen = Input.mousePosition;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
            Vector2 direction = new Vector2(
                            mouse.x - transform.position.x,
                            mouse.y - transform.position.y);
            float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90 - 180;
            foreach (Transform child in this.transform)
            {
                if (child.tag == "Cannon")
                {


                    if ((angle < -270) && (angle > -360))
                    {
                        angle = -270;
                    }
                    if (angle <= -360)
                    {
                        angle = -90;
                    }
                    child.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
            myTime = myTime + Time.deltaTime;

            if (GlobalValues.train.trainComponents[compId].health > 0)
            {
                if (Input.GetButton("Fire1") && myTime > nextFire)
                {
                    if (angle > -270)
                    {
                        Utils.playAudio(fireClip, audioSourceShort, false);
                        nextFire = myTime + fireDelta;
                        newProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
                        newProjectile.transform.position = transform.Find("turret1_cannon").transform.Find("FireSource").position;
                        newProjectile.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
                        Rigidbody2D pRB = newProjectile.GetComponent<Rigidbody2D>();
                        pRB.angularVelocity = 0f;
                        pRB.velocity = (direction * projectileSpeed);

                        // create code here that animates the newProjectile

                        nextFire = nextFire - myTime;
                        myTime = 0.0F;
                    }
                }
            }
        }
    }
}
