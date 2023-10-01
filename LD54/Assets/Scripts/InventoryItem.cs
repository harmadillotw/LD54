using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem 
{
    public int id;
    public bool train;
    public int traincomp;
    public int type;
    // 0 resource
    // 1 people
    // 2 person;
    public int resources;
    public int people;
    public int moral;
    public int person;

    public InventoryItem(int type, int resources, int people, int moral)
    {
        this.id = GlobalValues.cargoId++;
        this.type = type;
        this.resources = resources;
        this.people = people;
        this.moral = moral;
    }
    public InventoryItem(int type, int resources, int people, int moral, int person)
    {
        this.id = GlobalValues.cargoId++;
        this.type = type;
        this.resources = resources;
        this.people = people;
        this.moral = moral;
        this.person = person;
    }

}
