
using Godot;
using System;
using System.Collections.Generic;

public class Generation
{
	public int ID { get; init; }
	public TileGrid TileGrid { get; init; }
	public Storage Storage { get; init; }

	public int LengthInSeconds { get; set; }
	public int ElapsedSeconds { get; set; }
	public bool IsEnded { get { return LengthInSeconds - ElapsedSeconds <= 0; } }

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

	public Generation(Generation previous)
	{
		ID = previous.ID + 1;
		TileGrid = new(previous.TileGrid);
		Storage = previous.Storage;

		Vector2I gridSize = new(TileGrid.Grid.GetLength(0), TileGrid.Grid.GetLength(1));
		List<Vector2I> updateOrder = previous.OrderedTiles(gridSize);
		Random random = TileGrid.Random;
		
		foreach (Vector2I position in updateOrder)
		{
			ITile tile = TileGrid.Grid[position.X, position.Y];
			if (tile.Type is not TileType.Weed) { continue; }
		
			if (position.X - 1 >= 0 && TileGrid.Grid[position.X - 1, position.Y].Type.IsReplaceableByWeed())
			{
				float value = random.NextSingle();
				if (value < 0.05f) { TileGrid.SetCell(new(position.X - 1, position.Y), TileType.Weed); }
			}
		
			if (position.X + 1 < gridSize.X && TileGrid.Grid[position.X + 1, position.Y].Type.IsReplaceableByWeed())
			{
				float value = random.NextSingle();
				if (value < 0.05f) { TileGrid.SetCell(new(position.X + 1, position.Y), TileType.Weed); }
			}
		
			if (position.Y - 1 >= 0 && TileGrid.Grid[position.X, position.Y - 1].Type.IsReplaceableByWeed())
			{
				float value = random.NextSingle();
				if (value < 0.05f) { TileGrid.SetCell(new(position.X, position.Y - 1), TileType.Weed); }
			}
		
			if (position.Y + 1 < gridSize.Y && TileGrid.Grid[position.X, position.Y + 1].Type.IsReplaceableByWeed())
			{
				float value = random.NextSingle();
				if (value < 0.05f) { TileGrid.SetCell(new(position.X, position.Y + 1), TileType.Weed); }
			}
		}

		// ensure the length is at least 90 seconds
		LengthInSeconds = Math.Max(90, previous.LengthInSeconds - 15);
		ElapsedSeconds = 0;
	}

	public void SimulateSecond()
	{
		if (ElapsedSeconds >= LengthInSeconds) { return; }

		Vector2I size = new(TileGrid.Grid.GetLength(0), TileGrid.Grid.GetLength(1));
		List<Vector2I> tilesOrder = OrderedTiles(size);

		foreach (Vector2I position in tilesOrder)
		{
			ITile tile = TileGrid.Grid[position.X, position.Y];
			tile.SimulateSecond(ElapsedSeconds, position, TileGrid);
		}

		ElapsedSeconds++;
	}

	private List<Vector2I> OrderedTiles(Vector2I size)
	{
		List<Vector2I> tilesOrder = new(size.X * size.Y);

		// order the tiles by type
		for (int x = 0; x < size.X; x++)
		{
			for (int y = 0; y < size.Y; y++)
			{
				Vector2I entry = new(x, y);
				int index = tilesOrder.BinarySearch(entry,
					Comparer<Vector2I>.Create(
						(a, b) =>
						{
							TileType aType = TileGrid.Grid[a.X, a.Y].Type;
							TileType bType = TileGrid.Grid[b.X, b.Y].Type;

							int comp = aType.CompareTo(bType);
							if (comp is 0) { comp = a.Y.CompareTo(b.Y); }
							if (comp is 0) { comp = a.X.CompareTo(b.X); }
							return comp;
						}
				));

				if (index < 0) { tilesOrder.Insert(~index, entry); }
			}
		}

		return tilesOrder;
	}
}

