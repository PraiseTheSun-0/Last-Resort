using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public GameObject toBuild = null;

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
        if (toBuild != null)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GridController gridController = GameObject.Find("GridController").GetComponent<GridController>();
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                //Cell cell = gridController.team1_flowfield.GetCellFromWorldPos(position);
                if (gridController.team1_flowfield.GetCellFromWorldPos(position).cost != 255 && 
                    gridController.team1_flowfield.GetCellFromWorldPos(position + new Vector3(1, 0, 0)).cost != 255 &&
                    gridController.team1_flowfield.GetCellFromWorldPos(position + new Vector3(0, -1, 0)).cost != 255 &&
                    gridController.team1_flowfield.GetCellFromWorldPos(position + new Vector3(1, -1, 0)).cost != 255)

                {
                    position.x = Mathf.Floor(position.x) + 1;
                    position.y = Mathf.Floor(position.y);
                    Instantiate(toBuild, position, Quaternion.Euler(0, 0, 0));
                    toBuild = null;
                    gridController.updateDirection();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                toBuild = null;
            }
        }
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
