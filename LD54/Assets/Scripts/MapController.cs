using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public List<GameObject> mapButtons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i< mapButtons.Count; i++)
        {
            if (mapButtons[i].GetComponent<ButtonFlash>() != null)
            {
                mapButtons[i].GetComponent<ButtonFlash>().buttonNumber = i + 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectDestination(int destination)
    {
        GlobalValues.previousStation = GlobalValues.destination;
        GlobalValues.destination = destination;
        GlobalValues.stationInventory.Clear();
        if (destination == 2)
        {
            InventoryItem IItem = new InventoryItem(0, 100, 0, 0);
            GlobalValues.stationInventory.Add(IItem.id, IItem);
            IItem = new InventoryItem(1, 0, 10, 10);
            GlobalValues.stationInventory.Add(IItem.id, IItem);
        }
        else if (destination > 5)
        {
            int numItems = Random.Range(1, 5);
            for (int i=0;i<numItems; i++)
            {
                int itemType = Random.Range(0, 5);
                InventoryItem IItem = new InventoryItem(0, 100, 0, 0);
                switch (itemType)
                {
                    case 0:
                    case 1:
                        IItem = new InventoryItem(0, 100, 0, 0);
                        break;
                    case 2:
                    case 3:
                        int people = Random.Range(1, 11);
                        int isMoral = Random.Range(0, 2);
                        int moral = 0;
                        if (isMoral > 0)
                        {
                            moral = Random.Range(1, 6);
                        }
                        IItem = new InventoryItem(1, 0, people, moral);
                        break;
                    case 4:
                        int personType = Random.Range(0, 2);
                        IItem = new InventoryItem(2, 0, 0, 0, personType);
                        break;
                }
                GlobalValues.stationInventory.Add(IItem.id, IItem);
            }
        }
        else if (destination > 3)
        {
            int numItems = Random.Range(1, 3);
            for (int i = 0; i < numItems; i++)
            {
                int itemType = Random.Range(0, 2);
                InventoryItem IItem = new InventoryItem(0, 100, 0, 0);
                switch (itemType)
                {
                    case 0:
                        IItem = new InventoryItem(0, 100, 0, 0);
                        break;
                    case 1:
                        int people = Random.Range(1, 11);
                        int isMoral = Random.Range(0, 2);
                        int moral = 0;
                        if (isMoral >0)
                        {
                            moral = Random.Range(1, 6);
                        }
                        IItem = new InventoryItem(1, 0, people, moral);
                        break;
                }
                GlobalValues.stationInventory.Add(IItem.id, IItem);
            }
        }
            
        SceneManager.LoadScene("MainScene");
    }
}
