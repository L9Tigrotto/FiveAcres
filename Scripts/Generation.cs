
using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;

public class Generation
{
	public int ID { get; init; }
	public TileGrid TileGrid { get; init; }
	public Storage Storage { get; init; }

	public int LengthInSeconds { get; set; }
	public int ElapsedSeconds { get; set; }

	public Generation(Vector2I size, TileMapLayer tileMapLayer, Storage storage, int lengthInSeconds)
	{
		ID = 0;
		Random random = new();
		TileGrid = new(size, random, tileMapLayer);
		Storage = storage;

		// ensure the length is at least 90 seconds
		LengthInSeconds = Math.Max(90, lengthInSeconds);
		ElapsedSeconds = 0;
	}

	private Generation(Generation previous)
	{
		ID = previous.ID + 1;

		Vector2I size = new(previous.TileGrid.Grid.GetLength(0), previous.TileGrid.Grid.GetLength(1));
		TileGrid = new(previous.TileGrid);

		// ensure the length is at least 90 seconds
		LengthInSeconds = Math.Max(90, previous.LengthInSeconds - 15);
		ElapsedSeconds = 0;
	}

	public Generation Advance() { return new(this); }

	public bool SimulateSecond()
	{
		if (ElapsedSeconds >= LengthInSeconds) { return true; }

		Vector2I size = new(TileGrid.Grid.GetLength(0), TileGrid.Grid.GetLength(1));
		List<Vector2I> tilesOrder = OrderedTiles(size);

		foreach (Vector2I position in tilesOrder)
		{
			ITile tile = TileGrid.Grid[position.X, position.Y];
			tile.SimulateSecond(ElapsedSeconds, position, TileGrid);
		}

		ElapsedSeconds++;
		return ElapsedSeconds >= LengthInSeconds;
	}

	private List<Vector2I> OrderedTiles(Vector2I size)
	{
		List<Vector2I> tilesOrder = new(size.X * size.Y);

		// order the tiles by type
		for (int x = 0; x < size.X; x++)
		{
			for (int y = 0; y < size.Y; y++)
			{
				Vector2I entry = new Vector2I(x, y);
				int index = tilesOrder.BinarySearch(entry,
					Comparer<Vector2I>.Create(
						(a, b) =>
						{
							TileType aType = TileGrid.Grid[a.X, a.Y].Type;
							TileType bType = TileGrid.Grid[b.X, b.Y].Type;

							return aType.CompareTo(bType);
						}
				));

				if (index < 0) { tilesOrder.Insert(~index, entry); }
			}
		}

		return tilesOrder;
	}
}

