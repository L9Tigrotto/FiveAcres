

using Godot;
using System.Diagnostics;

public enum TileType
{
	Grass,
	//Soil,
	Water,
	Stone,
}

public static class TileTypeExtensions
{
	public static Vector2I ToAtlasCoord(this TileType type)
	{
		return type switch
		{
			TileType.Grass => new(1, 0),
			TileType.Stone => new(0, 1),
			TileType.Water => new(2, 3),
			_ => throw new UnreachableException()
		};
	}
}