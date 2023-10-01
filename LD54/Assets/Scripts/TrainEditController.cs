using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainEditController : MonoBehaviour
{
    public GameObject corePrefab;
    public GameObject emptyPrefab;
    public GameObject frontPrefab;
    public GameObject storagePrefab;
    public GameObject turretPrefab;

    public GameObject upgradePanel;
    public GameObject addCarriageButton;

    public TextMeshProUGUI resourcesText;
    public TextMeshProUGUI moralText;
    public Button addButton;
    public TMP_Dropdown carriageTypeDropdown;
    public TMP_Dropdown carriageUpgradeDropdown;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI repairText;

    private int selectedCarriage;

    private Color originalColor;
    private Color highlightColor = Color.yellow;

    private int selectedRepairCost;
    private int selectedUpgradeCost;
    // Start is called before the first frame update
    private void Awake()
    {
        //GlobalValues.Resources = 100;
        //GlobalValues.editMode = true;
        //GlobalValues.train = new Train();
        //TrainComponent front = new TrainComponent(1, 0);
        //front.front = true;
        //GlobalValues.train.trainComponents.Add(0, front);
        //GlobalValues.train.trainComponents.Add(1, new TrainComponent(-1, 0, 1, 1));
        //GlobalValues.train.trainComponents.Add(2, new TrainComponent(-2, 0, 2, 1));
        selectedCarriage = 2;
        //drawTrain();
        //for (int i = 0; i < GlobalValues.train.trainComponents.Count; i++)
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
        //            case 0:
        //                section = emptyPrefab;
        //                break;
        //            case 1:
        //                section = storagePrefab;
        //                break;
        //            case 2:
        //                section = turretPrefab;
        //                break;
        //        }
        //        //section = emptyPrefab;
        //    }
        //    GameObject instGO = Instantiate(section, new Vector3(this.transform.position.x + (comp.locationX * 1.9f), this.transform.position.y, this.transform.position.z), Quaternion.identity, corePrefab.transform);
        //    instGO.GetComponent<CarriageController>().compId = i;
        //    if (comp.type == 2)
        //    {
        //        instGO.GetComponent<TurretController>().compId = i;
        //    }
        //    instGO.GetComponentInChildren<Animator>().enabled = false;
        //    GlobalValues.train.trainGameObjects.Add(instGO);
        //}
    }
    void Start()
    {

        originalColor = corePrefab.GetComponent<SpriteRenderer>().color;
        drawTrain();
        if (GlobalValues.maxCarriages <= GlobalValues.currentCarriages)
        {
            addCarriageButton.SetActive(false);
        }
        else
        {
            addCarriageButton.SetActive(true);
        }
        //train =  new Train();
        //for (int i = 0; i < GlobalValues.train.trainComponents.Count; i++)
        //{
        //    TrainComponent comp = GlobalValues.train.trainComponents[i];
        //    GameObject section;
        //    if (comp.front)
        //    {
        //        section = frontPrefab;
        //    }
        //    else
        //    {
        //        section = emptyPrefab;
        //    }
        //    Instantiate(section, new Vector3(this.transform.position.x + comp.locationX, this.transform.position.y + comp.locationY, this.transform.position.z), Quaternion.identity, this.transform);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        resourcesText.text = "Resources: " + GlobalValues.Resources;
    }

    public void addCarriage()
    {
        if (GlobalValues.Resources > 200)
        {
            if (GlobalValues.train.trainComponents.Count >= 5)
            {
                addButton.enabled = false;
                return;
            }
            unHighlightCarriage(selectedCarriage);
            selectedCarriage = GlobalValues.train.trainComponents.Count;
            int location = GlobalValues.train.trainComponents.Count * -1;
            GlobalValues.train.trainComponents.Add(GlobalValues.train.trainComponents.Count, new TrainComponent(location, 0, 0, 1));
            GlobalValues.Resources -= 200;
            GlobalValues.currentCarriages++;
            if (GlobalValues.maxCarriages <= GlobalValues.currentCarriages)
            {
                addCarriageButton.SetActive(false);
            }
            else
            {
                addCarriageButton.SetActive(true);
            }
            if (GlobalValues.train.trainComponents.Count >= 5)
            {
                addButton.enabled = false;

            }
            redrawTrain();
            highlightCarriage(selectedCarriage);
        }
    }

    void redrawTrain()
    {
        for (int i = 0; i < GlobalValues.train.trainGameObjects.Count; i++)
        {
            Destroy(GlobalValues.train.trainGameObjects[i]);
        }
        GlobalValues.train.trainGameObjects = new List<GameObject>();
        drawTrain();

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
        highlightCarriage(selectedCarriage);
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void goStation()
    {
        SceneManager.LoadScene("StationScene");
    }

    private void highlightCarriage(int carriage)
    {
        //originalColor = GlobalValues.train.trainGameObjects[carriage].GetComponent<SpriteRenderer>().color;
        GlobalValues.train.trainGameObjects[carriage].GetComponent<SpriteRenderer>().color = highlightColor;
        updateChangeDropdown();
        descriptionText.text = Constants.carriageTypeNames[GlobalValues.train.trainComponents[selectedCarriage].type] + " Carriage, ";

        if (GlobalValues.train.trainComponents[selectedCarriage].type == 1)
        {
            if (GlobalValues.healthLevel < GlobalValues.maxUpgradeLevel)
            {
                upgradePanel.SetActive(true);
                int level = GlobalValues.healthLevel;
                descriptionText.text += " Level " + level;
                upgradeText.text = "" + level + "00";
                selectedUpgradeCost = 100 * level;
            }
            else
            {
                upgradePanel.SetActive(false);
                int level = GlobalValues.speedLevel;
                descriptionText.text += " Level " + level;
                
                upgradeText.text = "-";
            }
        }
        else if (GlobalValues.train.trainComponents[selectedCarriage].type == 2)
        {
            if (GlobalValues.speedLevel < GlobalValues.maxUpgradeLevel)
            {
                upgradePanel.SetActive(true);
                int level = GlobalValues.speedLevel;
                descriptionText.text += " Level " + level;
                upgradeText.text = "" + level + "00";
                selectedUpgradeCost = 100 * level;
            }
            else
            {
                upgradePanel.SetActive(false);
                int level = GlobalValues.speedLevel;
                descriptionText.text += " Level " + level;
                
                upgradeText.text = "-";
            }
        }
        if (GlobalValues.train.trainComponents[selectedCarriage].type == 1)
        {
            if (GlobalValues.speedLevel < GlobalValues.maxUpgradeLevel)
            {
                upgradePanel.SetActive(true);
            }
            else
            {
                upgradePanel.SetActive(false);
            }
        }

        //switch (GlobalValues.train.trainComponents[selectedCarriage].level)
        //{
        //    case 1:
        //        descriptionText.text += " Level 1";
        //        upgradeText.text = "$200";
        //        selectedUpgradeCost = 200;
        //        break;
        //    case 2:
        //        descriptionText.text += " Level 2";
        //        upgradeText.text = "$300";
        //        selectedUpgradeCost = 300;
        //        break;
        //    case 3:
        //        descriptionText.text += " Level 3";
        //        upgradeText.text = "";
        //        selectedUpgradeCost = 0;
        //        break;
        //}
        int repairNeeded = 20 - (GlobalValues.train.trainComponents[selectedCarriage].health );
        selectedRepairCost = repairNeeded / 2;
        repairText.text = "" + selectedRepairCost;

        //if (GlobalValues.train.trainComponents[selectedCarriage].level >= GlobalValues.maxUpgradeLevel)
        //{
        //    upgradePanel.SetActive(false);
        //}
        //else
        //{
        //    upgradePanel.SetActive(true);
        //}


    }
    private void unHighlightCarriage(int carriage)
    {
        GlobalValues.train.trainGameObjects[carriage].GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void next()
    {
        unHighlightCarriage(selectedCarriage);
        if (selectedCarriage > 1)
        {
            selectedCarriage--;
        }
        highlightCarriage(selectedCarriage);
    }
    public void previous()
    {
        unHighlightCarriage(selectedCarriage);
        if (selectedCarriage < (GlobalValues.train.trainGameObjects.Count - 1))
        {
            selectedCarriage++;
        }
        highlightCarriage(selectedCarriage);
    }
    

    private void updateChangeDropdown()
    {
        carriageTypeDropdown.ClearOptions();
        if (selectedCarriage != 0)
        {
            int selected = 0;
            List<string> options = new List<string>();
            for (int i = 0; i < Constants.carriageTypeNames.Length; i++)
            {
                string name = Constants.carriageTypeNames[i];
                if (GlobalValues.train.trainComponents[selectedCarriage].type != i)
                {
                    if (i != 0)
                    {
                        name += " " + Constants.carriageTypeCost[i];
                    }
                }
                else
                {
                    selected = i;
                }
                options.Add(name);
            }
            carriageTypeDropdown.AddOptions(options);
            carriageTypeDropdown.value = selected;
        }
    }

    public void repair()
    {
        if (GlobalValues.Resources > selectedRepairCost)
        {
            GlobalValues.train.trainComponents[selectedCarriage].health = 20;
            GlobalValues.Resources -= selectedRepairCost;
            unHighlightCarriage(selectedCarriage);
            highlightCarriage(selectedCarriage);
        }
    }
    public void upgrade()
    {
        if (GlobalValues.Resources >= selectedUpgradeCost)
        {
            GlobalValues.Resources -= selectedUpgradeCost;
            if (GlobalValues.train.trainComponents[selectedCarriage].type == 1)
            {
                GlobalValues.maxCarriageHealth += 10;
                for (int i = 0; i < GlobalValues.train.trainComponents.Count; i++)
                {
                    GlobalValues.train.trainComponents[i].health = GlobalValues.maxCarriageHealth;
                }
            }
            else if (GlobalValues.train.trainComponents[selectedCarriage].type == 2)
            {
                GlobalValues.maxSpeed += 5;
            }
        }
    }
    public void change()
    {
        if (carriageTypeDropdown.options[carriageTypeDropdown.value].text.StartsWith("Empty"))
        {
            GlobalValues.train.trainComponents[selectedCarriage].type = 0;
            redrawTrain();
        }
        else if (carriageTypeDropdown.options[carriageTypeDropdown.value].text.StartsWith("Cargo"))
        {
            if (GlobalValues.Resources >= 100)
            {
                GlobalValues.Resources -= 100;
                GlobalValues.train.trainComponents[selectedCarriage].type = 1;
                redrawTrain();
            }
        }
        else if (carriageTypeDropdown.options[carriageTypeDropdown.value].text.StartsWith("Turret"))
        {
            if (GlobalValues.Resources >= 150)
            {
                GlobalValues.Resources -= 150;
                GlobalValues.train.trainComponents[selectedCarriage].type = 2;
                redrawTrain();
            }

        }

    }

}
