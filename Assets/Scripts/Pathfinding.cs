using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfinding {

	public void Search(Grid grid, Tile start, Tile goal)
	{
		PriorityQueue frontier = new PriorityQueue();
		frontier.Enqueue(start);

		Dictionary<Tile, Tile> came_from = new Dictionary<Tile, Tile>();
		Dictionary<Tile, int> cost_so_far = new Dictionary<Tile, int>();

		cost_so_far[start] = 0;

		while (frontier.Count > 0)
		{
			var current = frontier.Dequeue();

			if (current.Equals(goal))
			{
				goal.GetComponent<SpriteRenderer>().color = Color.green;
				break;
			}

			foreach (Tile next in grid.AdjacentTiles(current))
			{
				int new_cost = cost_so_far[current] + 1;
				if ((!cost_so_far.ContainsKey(next)  || new_cost < cost_so_far[next]) && !next.obstacle)
				{
					cost_so_far[next] = new_cost;
					next.priority = new_cost + grid.Heuristic(goal, next);
					frontier.Enqueue(next);
					came_from[next] = current;
				}
			}
		}

		drawPath(came_from, start, goal);

	}

	public void drawPath(Dictionary<Tile, Tile> came_from, Tile start, Tile goal)
	{
		Tile current = goal;

		while (current != start)
		{
			if (current != goal)
				current.GetComponent<SpriteRenderer>().color = Color.magenta;

			current = came_from[current];
		}
	}

	public class PriorityQueue
	{
		private List<Tile> tiles = new List<Tile>();

		public void Enqueue(Tile tile)
		{
			tiles.Add(tile);
		}

		public Tile Dequeue()
		{
			int bestIndex = 0;

			for (int i = 0; i < tiles.Count; i++)
			{
				if (tiles[i].priority < tiles[bestIndex].priority)
				{
					bestIndex = i;
				}
			}

			Tile bestTile = tiles[bestIndex];
			tiles.RemoveAt(bestIndex);
			return bestTile;
		}

		public int Count
		{
			get { return tiles.Count; }
		}
	}

}
