using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContrioller : MonoBehaviour
{
    public GameObject tutText1;
    public GameObject tutText2;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalValues.destination == 2)
        {
            tutText1.SetActive(true);
        }
        if ((GlobalValues.destination == 3) && (GlobalValues.previousStation == 2))
        {
            tutText2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
