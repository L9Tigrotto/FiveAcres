
using Godot;
using System;

public class Generation
{
	public int ID { get; init; }
	public Random Random { get; set; }
	public TileGrid TileGrid { get; init; }

	public int LengthInSeconds { get; set; }

	public Generation(int id, Vector2I size, TileMapLayer tileMapLayer)
	{
		ID = id;
		Random = new();
		TileGrid = new(size, Random, tileMapLayer);
	}

	private Generation(Generation previous)
	{
		ID = previous.ID + 1;
		Random = new();

		Vector2I size = new(previous.TileGrid.Grid.GetLength(0), previous.TileGrid.Grid.GetLength(1));
		TileGrid = new(size, Random, previous.TileGrid.TileMapLayer);
	}

	public Generation Advance()
	{
		Generation generation = new(this);



		return generation;
	}
}

