using Godot;
using System;

public class TileGrid
{
	public ITile[,] Grid { get; init; }
	public TileMapLayer TileMapLayer { get; init; }

	public TileGrid(Vector2I size, Random random, TileMapLayer tileMapLayer)
	{
		Grid = new ITile[size.X, size.Y];
		TileMapLayer = tileMapLayer;

		// initialize the grid with empty tiles
		for (int x = 0; x < size.X; x++)
		{
			for (int y = 0; y < size.Y; y++)
			{
				Grid[x, y] = Tiles.Of(TileType.Empty);
			}
		}

		// fill the grid with grass
		for (int x = 0; x < size.X; x++)
		{
			for (int y = 0; y < size.Y; y++)
			{
				SetCell(new Vector2I(x, y), TileType.Grass);
			}
		}

		// place some trees
		int numberOfTrees = random.Next(1, 3);
		for (int i = 0; i < numberOfTrees; i++)
		{
			int x = random.Next(0 + 1, size.X - 1);
			int y = random.Next(0 + 1, size.Y - 1);
			bool isTreePlantable = Grid[x, y].Type == TileType.Grass;

			if (!isTreePlantable) { continue; }

			TileType treeType = Tiles.LateStageTreeTypes[random.Next(0, Tiles.LateStageTreeTypes.Length)];
			SetCell(new Vector2I(x, y), treeType);
		}

		// place some weeds
		int numberOfWeeds = random.Next(0, 2);
		for (int i = 0; i < numberOfTrees; i++)
		{
			int x = random.Next(0 + 1, size.X - 1);
			int y = random.Next(0 + 1, size.Y - 1);
			bool isWeedPlantable = Grid[x, y].Type == TileType.Grass;

			if (!isWeedPlantable) { continue; }

			TileType treeType = Tiles.LateStageTreeTypes[random.Next(0, Tiles.LateStageTreeTypes.Length)];
			SetCell(new Vector2I(x, y), treeType);
		}
	}

	public TileGrid(TileGrid previous)
	{
		Vector2I vector = new(previous.Grid.GetLength(0), previous.Grid.GetLength(1));
		Grid = new ITile[vector.X, vector.Y];
		TileMapLayer = previous.TileMapLayer;

		for (int x = 0; x < vector.X; x++)
		{
			for (int y = 0; y < vector.Y; y++)
			{
				Grid[x, y] = previous.Grid[x, y];
			}
		}
	}

	public void SetCell(Vector2I position, TileType type)
	{
		if (Grid[position.X, position.Y].Type == type) { return; }

		TileMapLayer.SetCell(position, 0, type.ToAtlasCoord());
		Grid[position.X, position.Y] = Tiles.Of(type);
	}
}

