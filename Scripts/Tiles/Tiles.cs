
using System.Collections.Generic;

public static class Tiles
{
	public static readonly Dictionary<TileType, ITile> AllTiles = new()
	{
		[TileType.Grass] = new GrassTile(),
		[TileType.Stone] = new StoneTile(),
		[TileType.Water] = new WaterTile(),
	};

	public static ITile Of(TileType type) { return AllTiles[type]; }
}

file class GrassTile() : ITile
{
	public TileType Type { get; } = TileType.Grass;
}

file class StoneTile() : ITile
{
	public TileType Type { get; } = TileType.Stone;
}

file class WaterTile() : ITile
{
	public TileType Type { get; } = TileType.Water;
}