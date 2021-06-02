using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    public int gold;
    public int crystals;
    public int income;
    public int team;
    public Text goldText;
    public Text crystalsText;
    private float incomeTimer = 0;
    public float incomeTime = 10f;

    private void Start()
    {
        gold = 250;
        crystals = 125;
        income = 5;
    }

    private void Update()
    {
        getIncome();
        goldText.text = gold.ToString();
        crystalsText.text = crystals.ToString();
    }

    private void getIncome()
    {
        incomeTimer += Time.deltaTime;
        if (incomeTimer > incomeTime)
        {
            incomeTimer = 0;
            gold += income;
        }
    }

    public void increaseIncome(int increase)
    {
        income += increase;
    }
}
