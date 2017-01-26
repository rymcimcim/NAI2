using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MapGeneratorController : MonoBehaviour {

	public GameObject gridPrefab;
	private GameObject gridGameObject;

	public GameObject camera;

	public GameObject gridSizeInput;
	public GameObject obstacleCountInput;

	public GameObject dialog;
	public GameObject dialogTextField;

	private Grid grid;

	public void generateGrid()
	{
		if (grid != null)
		{
			Destroy(gridGameObject);
		}

		gridGameObject = Instantiate(gridPrefab);
		grid = gridGameObject.GetComponent<Grid>();

		InputField obstacleInput = gridSizeInput.GetComponent<InputField>();

		if (obstacleInput.text != "")
		{
			int n = int.Parse(obstacleInput.text);

			if (n >= 10)
			{
				grid.createGrid(n, n);

				camera.transform.position = new Vector3(grid.transform.position.x + grid.width / 2, grid.transform.position.y + grid.height / 2, camera.transform.position.z);
			}
			else
			{
				dialogTextField.GetComponent<Text>().text = "Map dimensions must be at least 10";
				dialog.SetActive(true);
			}
		}
		else
		{
			dialogTextField.GetComponent<Text>().text = "You must provide a dimensions of map";
			dialog.SetActive(true);
		}

	}

	public void generateObstacles()
	{
		InputField obstacleInput = obstacleCountInput.GetComponent<InputField>();

		if(obstacleInput.text != "")
		{
			int count = int.Parse(obstacleInput.text);

			if (grid != null)
			{
				grid.generateObstacles(count);
				grid.drawObstacles();

				grid.generteStartTile();
				grid.generteGoalTile();
			}
			else
			{
				dialogTextField.GetComponent<Text>().text = "Map not yet generated";
				dialog.SetActive(true);
			}
		}
		else
		{
			dialogTextField.GetComponent<Text>().text = "You must provide a number for obstacles";
			dialog.SetActive(true);
		}


	}

	public void AStarSearch()
	{
		if (grid != null)
		{
			AStarPathfinding search = new AStarPathfinding();
			search.Search(grid, grid.startTile, grid.goalTile);
		}
		else
		{
			dialogTextField.GetComponent<Text>().text = "Map not yet generated";
			dialog.SetActive(true);
		}

	}
}
