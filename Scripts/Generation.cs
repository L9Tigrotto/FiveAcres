
using Godot;
using System;
using System.Collections.Generic;

public class Generation
{
	public int ID { get; init; }
	public TileGrid TileGrid { get; init; }

	public int LengthInSeconds { get; set; }
	public int ElapsedSeconds { get; set; }

	public Generation(Vector2I size, TileMapLayer tileMapLayer, int lengthInSeconds)
	{
		ID = 0;
		Random random = new();
		TileGrid = new(size, random, tileMapLayer);

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

	public Generation Advance()
	{
		Generation newGeneration = new(this);
		Vector2I size = new(newGeneration.TileGrid.Grid.GetLength(0), newGeneration.TileGrid.Grid.GetLength(1));
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
							TileType aType = newGeneration.TileGrid.Grid[a.X, a.Y].Type;
							TileType bType = newGeneration.TileGrid.Grid[b.X, b.Y].Type;

							return aType.CompareTo(bType);
						}
				));

				if (index < 0) { tilesOrder.Insert(~index, entry); }
			}
		}

		foreach (Vector2I position in tilesOrder)
		{
			ITile tile = newGeneration.TileGrid.Grid[position.X, position.Y];

			if (tile.Type == TileType.Grass) { continue; }
			if (tile.Type == TileType.Soil) { continue; }
			if (tile.Type == TileType.Stone) { continue; }
			if (tile.Type == TileType.Weed) { continue; }

			// plants
			if (tile.Type == TileType.EarlyStageWheat) { continue; }
			if (tile.Type == TileType.MidStageWheat) { continue; }
			if (tile.Type == TileType.LateStageWheat) { continue; }

			if (tile.Type == TileType.EarlyStageRice) { continue; }
			if (tile.Type == TileType.MidStageRice) { continue; }
			if (tile.Type == TileType.LateStageRice) { continue; }

			if (tile.Type == TileType.EarlyStageCarrot) { continue; }
			if (tile.Type == TileType.MidStageCarrot) { continue; }
			if (tile.Type == TileType.LateStageCarrot) { continue; }

			if (tile.Type == TileType.EarlyStagePotato) { continue; }
			if (tile.Type == TileType.MidStagePotato) { continue; }
			if (tile.Type == TileType.LateStagePotato) { continue; }

			// trees
			if (tile.Type == TileType.EarlyStageAppleTree) { continue; }
			if (tile.Type == TileType.MidStageAppleTree) { continue; }
			if (tile.Type == TileType.LateStageAppleTree) { continue; }

			if (tile.Type == TileType.EarlyStageCherryTree) { continue; }
			if (tile.Type == TileType.MidStageCherryTree) { continue; }
			if (tile.Type == TileType.LateStageCherryTree) { continue; }

			if (tile.Type == TileType.EarlyStagePeachTree) { continue; }
			if (tile.Type == TileType.MidStagePeachTree) { continue; }
			if (tile.Type == TileType.LateStagePeachTree) { continue; }

			if (tile.Type == TileType.EarlyStagePearTree) { continue; }
			if (tile.Type == TileType.MidStagePearTree) { continue; }
			if (tile.Type == TileType.LateStagePearTree) { continue; }
		}

		return newGeneration;
	}

	public bool SimulateSecond()
	{
		if (ElapsedSeconds >= LengthInSeconds) { return true; }

		Vector2I size = new(TileGrid.Grid.GetLength(0), TileGrid.Grid.GetLength(1));
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

		foreach (Vector2I position in tilesOrder)
		{
			ITile tile = TileGrid.Grid[position.X, position.Y];
			tile.SimulateSecond(ElapsedSeconds, position, TileGrid);
		}

		ElapsedSeconds++;
		return ElapsedSeconds >= LengthInSeconds;
	}
}

