using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid : MonoBehaviour {

	private List<Tile> tiles;
	private List<Obstacle> obstacles;

	public GameObject tilePrefab;

	public int width;
	public int height;

	public Tile startTile;
	public Tile goalTile;

	private enum ObstacleType
	{
		oneOne = 0,
		oneTwo = 1,
		twoOne = 2,
		twoTwo = 3
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public Grid()
	{
		tiles = new List<Tile>();
		obstacles = new List<Obstacle>();
	}

	public void createGrid(int x, int y)
	{
		tiles = new List<Tile>();

		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < y; j++)
			{
				GameObject tileObj = (GameObject)Instantiate(tilePrefab, new Vector3(i, j, 0.0f), new Quaternion());
				tileObj.transform.SetParent(transform);
				Tile tile = tileObj.GetComponent<Tile>();
				tile.pos = new Vector2(i, j);
				tiles.Add(tile);
			}
		}

		width = x;
		height = y;
	}

	public void generateObstacles(int count)
	{
		int obstaclesGenerated = 0;

		while (obstaclesGenerated < count)
		{
			int x = Random.Range(0, width - 1);
			int y = Random.Range(0, height - 1);

			Tile tile = getTileAt(x, y);

			if (!tile.obstacle)
			{
				if (AddObstacle(tile))
					obstaclesGenerated++;
			}
		}

	}

	public Tile generateRandomTile()
	{
		bool accessibleTile = false;

		while (!accessibleTile)
		{
			int x = Random.Range(0, width - 1);
			int y = Random.Range(0, height - 1);

			Tile tile = getTileAt(x, y);

			if (!tile.obstacle)
			{
				accessibleTile = true;
				return tile;
			}

		}

		return null;
	}

	public void generteStartTile()
	{
		Tile tile = generateRandomTile();

		if (tile != null)
		{
			startTile = tile;
			startTile.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	public void generteGoalTile()
	{
		Tile tile = generateRandomTile();

		if (tile != null)
		{
			goalTile = tile;
			goalTile.GetComponent<SpriteRenderer>().color = Color.green;
		}
	}

	public void drawObstacles()
	{
		foreach (Obstacle obstacle in obstacles)
		{
			for (int i = 0; i < obstacle.tiles.Count; i++)
			{
				obstacle.tiles[i].GetComponent<SpriteRenderer>().color = Color.gray;
			}

		}
	}

	public Tile getTileAt(int x, int y)
	{
		return tiles[x * width + y];
	}

	public bool AddObstacle(Tile tile)
	{
		Obstacle obstacle = new Obstacle();
		int obstacleType = Random.Range(0, 3);

		if (obstacleType == (int)ObstacleType.oneOne && !tile.obstacle)
		{
			obstacle.addTile(tile);
			tile.obstacle = true;
			obstacles.Add(obstacle);
			return true;
		}
		else if (obstacleType == (int)ObstacleType.oneTwo && !tile.obstacle)
		{
			if (AddOneTwoObstacle(tile, obstacle))
			{
				obstacle.addTile(tile);
				tile.obstacle = true;
				obstacles.Add(obstacle);
				return true;
			}

		}
		else if (obstacleType == (int)ObstacleType.twoOne && !tile.obstacle)
		{
			if (AddTwoOneObstacle(tile, obstacle))
			{
				obstacle.addTile(tile);
				tile.obstacle = true;
				obstacles.Add(obstacle);
				return true;
			}
		}
		else if (obstacleType == (int)ObstacleType.twoTwo && !tile.obstacle)
		{
			if (AddTwoTwoObstacle(tile, obstacle))
			{
				obstacle.addTile(tile);
				tile.obstacle = true;
				obstacles.Add(obstacle);
				return true;
			}
		}

		return false;
	}

	public bool AddOneTwoObstacle(Tile tile, Obstacle obstacle)
	{
		if (tile.pos.x + 1 < width)
		{
			Tile secondTile = getTileAt((int)tile.pos.x + 1, (int)tile.pos.y);

			if (!secondTile.obstacle)
			{
				obstacle.addTile(secondTile);
				secondTile.obstacle = true;
				return true;
			}
		}
		else if (tile.pos.x - 1 >= 0)
		{
			Tile secondTile = getTileAt((int)tile.pos.x - 1, (int)tile.pos.y);

			if (!secondTile.obstacle)
			{
				obstacle.addTile(secondTile);
				secondTile.obstacle = true;
				return true;
			}
		}

		return false;
	}

	public bool AddTwoOneObstacle(Tile tile, Obstacle obstacle)
	{
		if (tile.pos.y + 1 < height)
		{
			Tile secondTile = getTileAt((int)tile.pos.x, (int)tile.pos.y + 1);

			if (!secondTile.obstacle)
			{
				obstacle.addTile(secondTile);
				secondTile.obstacle = true;
				return true;
			}
		}
		else if (tile.pos.y - 1 >= 0)
		{
			Tile secondTile = getTileAt((int)tile.pos.x, (int)tile.pos.y - 1);

			if (!secondTile.obstacle)
			{
				obstacle.addTile(secondTile);
				secondTile.obstacle = true;
				return true;
			}
		}

		return false;
	}

	public bool AddTwoTwoObstacle(Tile tile, Obstacle obstacle)
	{
		List<Tile> tempTiles = new List<Tile>();

		if (tile.pos.x + 1 < width && tile.pos.y + 1 < height)
		{
			tempTiles.Add(getTileAt((int)tile.pos.x + 1, (int)tile.pos.y));
			tempTiles.Add(getTileAt((int)tile.pos.x, (int)tile.pos.y + 1));
			tempTiles.Add(getTileAt((int)tile.pos.x + 1, (int)tile.pos.y + 1));
		}
		else if (tile.pos.x + 1 >= 0 && tile.pos.y - 1 < height)
		{
			tempTiles.Add(getTileAt((int)tile.pos.x + 1, (int)tile.pos.y));
			tempTiles.Add(getTileAt((int)tile.pos.x, (int)tile.pos.y - 1));
			tempTiles.Add(getTileAt((int)tile.pos.x + 1, (int)tile.pos.y - 1));
		}
		else if (tile.pos.x - 1 < width && tile.pos.y + 1 < height)
		{
			tempTiles.Add(getTileAt((int)tile.pos.x - 1, (int)tile.pos.y));
			tempTiles.Add(getTileAt((int)tile.pos.x, (int)tile.pos.y + 1));
			tempTiles.Add(getTileAt((int)tile.pos.x - 1, (int)tile.pos.y + 1));
		}
		else if (tile.pos.x - 1 >= 0 && tile.pos.y - 1 < height)
		{
			tempTiles.Add(getTileAt((int)tile.pos.x - 1, (int)tile.pos.y));
			tempTiles.Add(getTileAt((int)tile.pos.x, (int)tile.pos.y + 1));
			tempTiles.Add(getTileAt((int)tile.pos.x + 1, (int)tile.pos.y - 1));
		}

		if (tempTiles.Count > 0)
		{
			bool obstacles = false;
			foreach (Tile tempTile in tempTiles)
			{
				if (tempTile.obstacle)
					obstacles = true;
			}

			if (!obstacles)
			{
				foreach (Tile tempTile in tempTiles)
				{
					obstacle.addTile(tempTile);
					tempTile.obstacle = true;

				}

				return true;
			}
		}

		return false;
	}

	public List<Tile> AdjacentTiles(Tile tile)
	{
		List<Tile> tiles = new List<Tile>();

		if (tile.pos.x + 1 < width)
		{
			tiles.Add(getTileAt((int)tile.pos.x + 1, (int)tile.pos.y));
		}

		if (tile.pos.x - 1 >= 0)
		{
			tiles.Add(getTileAt((int)tile.pos.x - 1, (int)tile.pos.y));
		}

		if (tile.pos.y + 1 < height)
		{
			tiles.Add(getTileAt((int)tile.pos.x, (int)tile.pos.y + 1));
		}

		if (tile.pos.y - 1 >= 0)
		{
			tiles.Add(getTileAt((int)tile.pos.x, (int)tile.pos.y - 1));
		}

		return tiles;
	}

	public int Heuristic(Tile tile1, Tile tile2)
	{
		return (int)(Mathf.Abs(tile1.pos.x - tile2.pos.x) + Mathf.Abs(tile1.pos.y - tile2.pos.y));
	}
}
