using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public int gold;
    public int crystals;
    public int income;
    public int team;

    private void Start()
    {
        gold = 250;
        crystals = 125;
        income = 5;
    }
}
