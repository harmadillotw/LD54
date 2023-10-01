using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train 
{
    public Dictionary<int, TrainComponent> trainComponents;
    public List<GameObject> trainGameObjects;


    public int size = 2;

    public Train()
    {
        trainComponents = new Dictionary<int, TrainComponent>();
        trainGameObjects = new List<GameObject>();

    }
}
