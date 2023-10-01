using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StationController : MonoBehaviour
{
    public GameObject corePrefab;
    public GameObject emptyPrefab;
    public GameObject frontPrefab;
    public GameObject storagePrefab;
    public GameObject turretPrefab;

    public GameObject InvEmptyPrefab;
    public GameObject InvFrontPrefab;
    public GameObject InvCorePrefab;
    public GameObject InvTurretPrefab;
    public GameObject Carriage2Prefab;
    public GameObject Carriage4Prefab;

    public GameObject ResourcePrefab;
    public GameObject PeoplePrefab;
    public GameObject PersonPrefab;


    public GameObject stationInventory;
    public GameObject trainInventory;

    public GameObject tutorialtext1;
    public GameObject tutorialtext2;

    public TextMeshProUGUI resourcesText;
    public TextMeshProUGUI peopleText;
    public TextMeshProUGUI moralText;

    public GameObject upgradeButton;

    //private int selectedCarriage;
    private void Awake()
    {
        if (GlobalValues.previousStation == 1)
        {
            GlobalValues.train.trainComponents = new Dictionary<int, TrainComponent>();
            TrainComponent front = new TrainComponent(1, 0);
            front.front = true;
            GlobalValues.train.trainComponents.Add(0, front);
            TrainComponent trainComp = new TrainComponent(-1, 0, 1, 1);
            trainComp.inventorySlots = 2;
            GlobalValues.train.trainComponents.Add(1, trainComp);
            trainComp = new TrainComponent(-2, 0, 2, 1);
            GlobalValues.train.trainComponents.Add(2, trainComp);
        }
        if (GlobalValues.destination == 3)
        {
            upgradeButton.SetActive(true);
            foreach ( var tComp in GlobalValues.train.trainComponents)
            {
                foreach (var invItem in tComp.Value.inventory)
                {
                    GlobalValues.Resources += invItem.Value.resources;
                    GlobalValues.People += invItem.Value.people;
                    GlobalValues.Moral += invItem.Value.moral;
                    if (tComp.Value.type == 2)
                    {
                        GlobalValues.maxUpgradeLevel++;
                    }

                }
                tComp.Value.inventory = new Dictionary<int, InventoryItem>(); ;
            }
            resourcesText.text = "Resources: " + GlobalValues.Resources;
            peopleText.text = "Resources: " + GlobalValues.People;
            moralText.text = "Moral: " + GlobalValues.Moral;
            if (GlobalValues.Moral <= 0)
            {
                GlobalValues.lost = true;
                SceneManager.LoadScene("EndGameScene");
            }
        }
        resourcesText.text = "Resources: " + GlobalValues.Resources;
        peopleText.text = "Resources: " + GlobalValues.People;
        moralText.text = "Moral: " + GlobalValues.Moral;
        //GlobalValues.Resources = 100;
        //GlobalValues.editMode = true;
        //GlobalValues.train = new Train();
        //TrainComponent front = new TrainComponent(1, 0);
        //front.front = true;
        //GlobalValues.train.trainComponents.Add(0, front);
        //TrainComponent trainComp = new TrainComponent(-1, 0, 1, 1);
        //trainComp.inventorySlots = 2;
        //InventoryItem IItem = new InventoryItem(0, 100, 0, 0);
        //IItem.train = true;
        //IItem.traincomp = 1;
        //trainComp.inventory.Add(IItem.id, IItem);
        //IItem = new InventoryItem(1, 0, 5, 4);
        //IItem.train = true;
        //IItem.traincomp = 1;
        //trainComp.inventory.Add(IItem.id, IItem);
        //GlobalValues.train.trainComponents.Add(1, trainComp);

        //trainComp = new TrainComponent(-2, 0, 2, 1);

        //GlobalValues.train.trainComponents.Add(2, trainComp);

    }
        // Start is called before the first frame update
        void Start()
    {
        GlobalValues.editMode = true;
        drawTrain();
        drawTrainInventory();
        drawStationInventory();
        if (GlobalValues.previousStation == 1)
        {
            tutorialtext1.SetActive(true);
        }
        if (GlobalValues.previousStation == 2)
        {
            tutorialtext2.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void drawStationInventory()
    {
        foreach (var statInv in  GlobalValues.stationInventory)
        {
            InventoryItem item = statInv.Value;
            GameObject invPrefab = ResourcePrefab;
            switch (item.type)
            {
                case 0:
                    invPrefab = ResourcePrefab;
                    break;
                case 1:
                    invPrefab = PeoplePrefab;
                    break;
                case 2:
                    invPrefab = PersonPrefab;
                    break;

            }
            foreach (Transform child in stationInventory.transform)
            {
                if (child.childCount == 0)
                {
                    GameObject invGO = Instantiate(invPrefab, child);
                    invGO.GetComponent<cargoDetails>().cargoId = item.id;
                    invGO.GetComponent<cargoDetails>().item = item;
                    break;
                }
            }
         }
    }
        void drawTrainInventory()
    {
        for (int i = 0; i<GlobalValues.train.trainComponents.Count; i++)
        {
            TrainComponent comp = GlobalValues.train.trainComponents[i];
            GameObject section = emptyPrefab;
            if (comp.type == 1)
            {
                switch (comp.level)
                {
                    case 1:
                        section = Carriage2Prefab;
                        break;
                    case 2:
                        section = Carriage4Prefab;
                        break;
                }
                GameObject instGO = Instantiate(section, trainInventory.transform);
                instGO.GetComponent<carriageID>().carriageId = i;
                foreach(var item in comp.inventory)
                {
                    GameObject invPrefab = ResourcePrefab;
                    switch(item.Value.type)
                    {
                        case 0:
                            invPrefab = ResourcePrefab;
                            break;
                        case 1:
                            invPrefab = PeoplePrefab;
                            break;
                        case 2:
                            invPrefab = PersonPrefab;
                            break;

                    }
                    foreach (Transform child in instGO.transform)
                    {
                        if (child.childCount == 0)
                        {
                            GameObject invGO = Instantiate(invPrefab, child);
                            invGO.GetComponent<cargoDetails>().cargoId = item.Value.id;
                            invGO.GetComponent<cargoDetails>().item = item.Value;
                            break;
                        }
                    }
                    //GameObject invGO = Instantiate(invPrefab, instGO.transform);
                }
            }
            //GameObject section;
            //section = emptyPrefab;

            //switch (comp.type)
            //{
            //    case 0:
            //        section = InvEmptyPrefab;
            //        break;
            //    case 1:
            //        section = Carriage2Prefab;
            //        break;
            //    case 2:
            //        section = InvTurretPrefab;
            //        break;
            //}
            //if (comp.front)
            //{
            //    section = InvFrontPrefab;
            //}
            

        }
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
            instGO.GetComponentInChildren<Animator>().enabled = false;
            GlobalValues.train.trainGameObjects.Add(instGO);
        }
    }

    public void nextStation()
    {
        SceneManager.LoadScene("StationScene");
    }
    public void exitStation()
    {
        if (GlobalValues.destination == 2)
        {
            GlobalValues.previousStation = GlobalValues.destination;
            SceneManager.LoadScene("MapSceneTut2");
        }
        else
        {
            GlobalValues.previousStation = GlobalValues.destination;
            SceneManager.LoadScene("MapScene");
        }

    }
    public void upgrade()
    {
        SceneManager.LoadScene("TrainScene");
    }
    public void endGame()
    {
        SceneManager.LoadScene("EndGameScene");
    }
}
