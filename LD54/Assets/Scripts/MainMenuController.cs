using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject tutorialPanel;

    public GameObject corePrefab;
    public GameObject emptyPrefab;
    public GameObject frontPrefab;
    public GameObject storagePrefab;
    public GameObject turretPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GlobalValues.maxCarriageHealth = 20;
        GlobalValues.maxSpeed = 15;
        GlobalValues.Resources = 100;
        GlobalValues.editMode = false;
        GlobalValues.stationInventory = new Dictionary<int, InventoryItem>();

        GlobalValues.train = new Train();
        TrainComponent front = new TrainComponent(1, 0);
        front.front = true;
        GlobalValues.train.trainComponents.Add(0, front);
        TrainComponent trainComp = new TrainComponent(-1, 0, 1, 1);
        GlobalValues.train.trainComponents.Add(1, trainComp);

        trainComp = new TrainComponent(-2, 0, 2, 1);

        GlobalValues.train.trainComponents.Add(2, trainComp);
        drawTrain();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void start()
    {
        SceneManager.LoadScene("TrainScene");
    }

    public void credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void exit()
    {
        Application.Quit();
    }

    public void options()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void newGame()
    {
        tutorialPanel.SetActive(true);
    }
    public void newGameWithTutorial()
    {
        GlobalValues.healthLevel = 1;
        GlobalValues.speedLevel = 1;
        GlobalValues.maxCarriageHealth = 20;
        GlobalValues.maxSpeed = 15;
        GlobalValues.lost = false;
        GlobalValues.maxCarriages = 3;
        GlobalValues.currentCarriages = 2;
        GlobalValues.maxUpgradeLevel = 1;
        GlobalValues.destination = 1;
        GlobalValues.Resources = 0;
        GlobalValues.Moral = 0;
        GlobalValues.editMode = false;
        GlobalValues.stationInventory = new Dictionary<int, InventoryItem>();
        GlobalValues.train = new Train();
        TrainComponent front = new TrainComponent(1, 0);
        front.front = true;
        GlobalValues.train.trainComponents.Add(0, front);
        TrainComponent trainComp = new TrainComponent(-1, 0, 0, 1);
        GlobalValues.train.trainComponents.Add(1, trainComp);

        trainComp = new TrainComponent(-2, 0, 0, 1);

        GlobalValues.train.trainComponents.Add(2, trainComp);
        SceneManager.LoadScene("MapSceneTut1");
    }
    public void newGameNoTutorial()
    {
        GlobalValues.healthLevel = 1;
        GlobalValues.speedLevel = 1;
        GlobalValues.maxCarriageHealth = 20;
        GlobalValues.maxSpeed = 15;
        GlobalValues.lost = false;
        GlobalValues.maxCarriages = 3;
        GlobalValues.currentCarriages = 2;
        GlobalValues.maxUpgradeLevel = 2;
        GlobalValues.destination = 3;
        GlobalValues.Resources = 100;
        GlobalValues.Moral = 10;
        GlobalValues.People = 10;
        GlobalValues.editMode = false;
        GlobalValues.stationInventory = new Dictionary<int, InventoryItem>();
        GlobalValues.train = new Train();
        TrainComponent front = new TrainComponent(1, 0);
        front.front = true;
        GlobalValues.train.trainComponents.Add(0, front);
        TrainComponent trainComp = new TrainComponent(-1, 0, 1, 1);
        GlobalValues.train.trainComponents.Add(1, trainComp);
        trainComp = new TrainComponent(-2, 0, 2, 1);

        GlobalValues.train.trainComponents.Add(2, trainComp);

        SceneManager.LoadScene("StationScene");
    }

    void drawTrain()
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
                    case 0:
                        section = emptyPrefab;
                        break;
                    case 1:
                        section = storagePrefab;
                        break;
                    case 2:
                        section = turretPrefab;
                        break;
                }
                //section = emptyPrefab;
            }
            GameObject instGO = Instantiate(section, new Vector3(corePrefab.transform.position.x + (comp.locationX * 1.9f), corePrefab.transform.position.y, corePrefab.transform.position.z), Quaternion.identity, corePrefab.transform);
            instGO.GetComponent<CarriageController>().compId = i;
            if (comp.type == 2)
            {
                instGO.GetComponent<TurretController>().compId = i;
            }
            instGO.GetComponentInChildren<Animator>().enabled = true;
            GlobalValues.train.trainGameObjects.Add(instGO);
        }
    }
}
