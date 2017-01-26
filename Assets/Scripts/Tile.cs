using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Vector2 pos;
	public bool obstacle;
	public int priority;

	public Tile(int x, int y)
	{
		pos = new Vector2(x, y);
	}

	public Tile(int x, int y, bool obst)
	{
		pos = new Vector2(x, y);
		obstacle = obst;
	}
}
