using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle {

	public List<Tile> tiles;

	public Obstacle()
	{
		tiles = new List<Tile>();
	}

	public Obstacle(List<Tile> tiles)
	{
		this.tiles = tiles;
	}

	public void addTile(Tile tile)
	{
		tiles.Add(tile);
	}
}
