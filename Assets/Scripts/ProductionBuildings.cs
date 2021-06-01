using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuildings : MonoBehaviour
{

    public GameObject unitToSpawn;
    private Vector3 offset = new Vector3(2.0f, 0.5f);
    private float spawnTimer = 0;
    public float spawnTime = 1.5f;

    public void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnTime)
        {
            spawnUnit();
            spawnTimer = 0;
        }
    }
    public void spawnUnit()
    {

        GameObject newUnit = Instantiate(unitToSpawn, transform.position + offset, Quaternion.identity);
        newUnit.GetComponent<Unit>().team = GetComponent<Building>().team;
        if (newUnit.GetComponent<Unit>().team == 0) newUnit.layer = 10;
        else newUnit.layer = 12;
    }
}
