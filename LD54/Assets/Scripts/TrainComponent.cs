using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainComponent 
{
    public int locationX;
    public int locationY;
    public int component;
    public bool front;
    public int type;
    public int level;
    public int health;
    public int inventorySlots;
    public Dictionary<int,InventoryItem> inventory;


    public TrainComponent()
    {
        locationX = 0;
        component = 0;
        initialise();
    }
    public TrainComponent(int locationX)
    {
        this.locationX = locationX;
        component = 0;
        initialise();
    }
    public TrainComponent(int locationX, int component)
    {
        this.locationX = locationX;
        this.component = component;
        front = false;
        initialise();
    }
    public TrainComponent(int locationX, int component, int type, int level)
    {
        this.locationX = locationX;
        this.component = component;
        this.type = type;
        this.level = level;
        initialise();
    }
    void initialise()
    {
        health = GlobalValues.maxCarriageHealth;
        front = false;
        locationY = 0;
        inventory = new Dictionary<int, InventoryItem>();
    }
}
