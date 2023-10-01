using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float xVelocity;
    public GameObject corePrefab;
    public GameObject emptyPrefab;
    public GameObject frontPrefab;
    public GameObject storagePrefab;
    public GameObject turretPrefab;

    public GameObject wispPrefab;
    public GameObject barracadePrefab;

    public TextMeshProUGUI speedText;

    //private float speed;
    private float maxspeed;

    private void Awake()
    {
        GlobalValues.editMode = false;

        if (GlobalValues.previousStation == 1)
        {
            Instantiate(barracadePrefab, new Vector3(49f, -1.792f, 0f), Quaternion.identity);
            // nothing for now
        }
        else if (GlobalValues.previousStation == 2)
        {
            Instantiate(barracadePrefab, new Vector3(39f, -1.792f, 0f), Quaternion.identity);
            Instantiate(barracadePrefab, new Vector3(69f, -1.792f, 0f), Quaternion.identity);
            GameObject wispGO = Instantiate(wispPrefab, new Vector3(20f, 5.5f, 0f), Quaternion.identity);
            wispGO.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            int numbarricaded = Random.Range(1, GlobalValues.destination);

            for (int i = 0; i < numbarricaded; i++)
            {
                float x = Random.Range(30f, 90f);
                Instantiate(barracadePrefab, new Vector3(x, -1.792f, 0f), Quaternion.identity);
            }
            for (int i=0; i<(GlobalValues.destination -1); i++)
            {
                float x = Random.Range(12f, 85f);
                float y = Random.Range(1.5f, 8f);
                GameObject wispGO = Instantiate(wispPrefab, new Vector3(x, y, 0f), Quaternion.identity);

                wispGO.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
        //GlobalValues.editMode = false;
        //GlobalValues.train = new Train();
        //TrainComponent front = new TrainComponent(1, 0);
        //front.front = true;
        //GlobalValues.train.trainComponents.Add(0,front);
        //GlobalValues.train.trainComponents.Add(1,new TrainComponent(-1, 0, 1, 1));
        //GlobalValues.train.trainComponents.Add(2,new TrainComponent(-2, 0, 2, 1));

        //for (int i= 0;i < GlobalValues.train.trainComponents.Count; i++)
        //{
        //    TrainComponent comp = GlobalValues.train.trainComponents[i];
        //    GameObject section;
        //    section = emptyPrefab;
        //    if (comp.front)
        //    {
        //        section = frontPrefab;
        //    }
        //    else
        //    {
        //        switch (comp.type)
        //        {
        //            case 1:
        //                section = storagePrefab;
        //                break;
        //            case 2:
        //                section = turretPrefab;
        //                break;
        //        }
        //        //section = emptyPrefab;
        //    }
        //    GameObject instGO = Instantiate(section, new Vector3(this.transform.position.x + (comp.locationX * 1.9f), this.transform.position.y, this.transform.position.z), Quaternion.identity, this.transform);
        //    instGO.GetComponent<CarriageController>().compId = i;
        //    if (comp.type == 2)
        //    {
        //        instGO.GetComponent<TurretController>().compId = i;
        //    }
        //    GlobalValues.train.trainGameObjects.Add(instGO);
        //}
    }
    // Start is called before the first frame update
    void Start()
    {


        //GlobalValues.train = new Train();
        //GlobalValues.train.trainComponents.Add(new TrainComponent(1,1));
        //GlobalValues.train.trainComponents.Add(new TrainComponent(-1, 1));
        rb = GetComponentInChildren<Rigidbody2D>();
        xVelocity = rb.velocity.x;
        maxspeed = GlobalValues.maxSpeed;
        GlobalValues.speed = 0;
        speedText.text = "Speed: " + GlobalValues.speed + "/" + maxspeed;
        
        drawTrain();
        //Instantiate(corePrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {

            GlobalValues.speed += 5f;
            if (GlobalValues.speed >= maxspeed)
            {
                GlobalValues.speed = maxspeed;
            }

            
        }
        if (Input.GetKeyUp(KeyCode.A))
        {

            GlobalValues.speed -= 5f;
            if (GlobalValues.speed < 0)
            {
                GlobalValues.speed = 0;
            }


        }
        speedText.text = "Speed: " + GlobalValues.speed + "/" + maxspeed;
        rb.velocity = new Vector2(GlobalValues.speed, rb.velocity.y);
        xVelocity = rb.velocity.x;
    }

    private void drawTrain()
    {
        GlobalValues.train.trainGameObjects = new List<GameObject>();
        for (int i = 0; i < GlobalValues.train.trainComponents.Count; i++)
        {
            TrainComponent comp = GlobalValues.train.trainComponents[i];
            GameObject section;
            section = emptyPrefab;
            if (comp.front)
            {
                section = frontPrefab;
            }
            else
            {
                switch (comp.type)
                {
                    case 1:
                        section = storagePrefab;
                        break;
                    case 2:
                        section = turretPrefab;
                        break;
                }
                //section = emptyPrefab;
            }
            GameObject instGO = Instantiate(section, new Vector3(this.transform.position.x + (comp.locationX * 1.9f), this.transform.position.y, this.transform.position.z), Quaternion.identity, this.transform);
            instGO.GetComponent<CarriageController>().compId = i;
            if (comp.type == 2)
            {
                instGO.GetComponent<TurretController>().compId = i;
            }
            GlobalValues.train.trainGameObjects.Add(instGO);
        }
    }

    
}
