using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public GridController gridController;
    public GameObject unitPrefab;
    public int numUnitsPerSpawn;
    public float moveSpeed;

    private List<GameObject> unitsInGame;

	private void Awake()
	{
		unitsInGame = new List<GameObject>();
	}

    private void Start()
    {
		gridController = GameObject.Find("GridController").GetComponent<GridController>();
    }

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SpawnUnits();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			DestroyUnits();
		}
	}

	private void FixedUpdate()
	{
		//if (gridController.curFlowField == null) { return; }
		//foreach (GameObject unit in unitsInGame)
		//{
		//	Cell cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
		//	Vector2 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y);
		//	unit.GetComponent<Unit>().FlowFieldMove(moveDirection);
		//}
	}

	public Vector2 whereToMove(Vector2 pos, int teamID)
    {
		Cell cellBelow = gridController.team1_flowfield.GetCellFromWorldPos(pos);
		if (teamID == 1) cellBelow = gridController.team2_flowfield.GetCellFromWorldPos(pos);
		return new Vector2(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y).normalized;
    }

	private void SpawnUnits()
	{
		Vector2Int gridSize = gridController.gridSize;
		float nodeRadius = gridController.cellRadius;
		Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeRadius * 2 + nodeRadius, gridSize.y * nodeRadius * 2 + nodeRadius);
		int colMask = LayerMask.GetMask("Impassible", "Units");
		Vector3 newPos;
		for (int i = 0; i < numUnitsPerSpawn; i++)
		{
			GameObject newUnit = Instantiate(unitPrefab);
			newUnit.transform.parent = transform;
			unitsInGame.Add(newUnit);
			do
			{
				newPos = new Vector3(Random.Range(0, maxSpawnPos.x), Random.Range(0, maxSpawnPos.y), 0);
				newUnit.transform.position = newPos;
			}
			while (Physics2D.OverlapCircleAll(newPos, 0.25f, colMask).Length > 0);
		}
	}

	private void DestroyUnits()
	{
		foreach (GameObject go in unitsInGame)
		{
			Destroy(go);
		}
		unitsInGame.Clear();
	}
}
