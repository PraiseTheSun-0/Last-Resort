using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField team1_flowfield;
	public FlowField team2_flowfield;
	public GridDebug gridDebug;


	private Cell[,] grid;



	private void InitializeFlowField()
	{
        team1_flowfield = new FlowField(cellRadius, gridSize);
		team1_flowfield.CreateGrid();
		team2_flowfield = new FlowField(cellRadius, gridSize);
		team2_flowfield.CreateGrid();
		gridDebug.SetFlowField(team1_flowfield);
	}


    private void Start()
    {
		InitializeFlowField();
		team1_flowfield.CreateCostField();
		team2_flowfield.CreateCostField();

		Cell destinationCell_team1 = team1_flowfield.grid[70,9];
		team1_flowfield.CreateIntegrationField(destinationCell_team1);

		Cell destinationCell_team2 = team2_flowfield.grid[5, 9];
		team2_flowfield.CreateIntegrationField(destinationCell_team2);

		team1_flowfield.CreateFlowField();
		team2_flowfield.CreateFlowField();

		gridDebug.DrawFlowField();
	}

    private void Update()
	{
		//if (Input.GetMouseButtonDown(0))
		//{
		//	InitializeFlowField();
		//	team1_flowfield.CreateCostField();
		//	team2_flowfield.CreateCostField();
		//
		//	Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
		//	Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
		//	Cell destinationCell = curFlowField.GetCellFromWorldPos(worldMousePos);
		//	curFlowField.CreateIntegrationField(destinationCell);
		//
		//	team1_flowfield.CreateFlowField();
		//	team2_flowfield.CreateFlowField();
		//
		//	gridDebug.DrawFlowField();
		//}
		//if (Input.GetMouseButtonDown(0))
		//{
		//	InitializeFlowField();
		//
		//	curFlowField.CreateCostField();
		//
		//	Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
		//	Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
		//	Cell destinationCell = curFlowField.GetCellFromWorldPos(worldMousePos);
		//	curFlowField.CreateIntegrationField(destinationCell);
		//
		//	curFlowField.CreateFlowField();
		//
		//	gridDebug.DrawFlowField();
		//}
	}
}
