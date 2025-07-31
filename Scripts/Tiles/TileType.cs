

using Godot;
using System.Diagnostics;

public enum TileType
{
	Empty = -1,		// used to indicate an empty tile

	Grass = 0,      // hoe card can transform soil grass in soil
	Soil = 1,		// soil can accomodate plants and trees
	Stone = 2,		// stone can be removed with a pickaxe card
	//Water,

	Weed = 10,      // weed spreads to adjacent soil tiles and can "eat" plants and trees

	// early stage plants
	EarlyStageWheat = 20,
	EarlyStageRice = 21,
	EarlyStageCarrot = 22,
	EarlyStagePotato = 23,

	// mid stage plants
	MidStageWheat = 30,
	MidStageRice = 31,
	MidStageCarrot = 32,
	MidStagePotato = 33,
	
	// late stage plants
	LateStageWheat = 40,
	LateStageRice = 41,
	LateStageCarrot = 42,
	LateStagePotato = 43,

	// early stage trees
	EarlyStageAppleTree = 50,
	EarlyStageCherryTree = 51,
	EarlyStagePeachTree = 52,
	EarlyStagePearTree = 53,

	// mid stage trees
	MidStageAppleTree = 60,
	MidStageCherryTree = 61,
	MidStagePeachTree = 62,
	MidStagePearTree = 63,

	// late stage trees
	LateStageAppleTree = 70,
	LateStageCherryTree = 71,
	LateStagePeachTree = 72,
	LateStagePearTree = 73,
}

public static class TileTypeExtensions
{
	public static Vector2I ToAtlasCoord(this TileType type)
	{
		return type switch
		{
			TileType.Grass => new(1, 0),
			TileType.Soil => new(2, 0),
			TileType.Stone => new(0, 1),
			//TileType.Water => new(2, 3),

			TileType.Weed => new(2, 3),

			// early stage seeds
			TileType.EarlyStageWheat => new(2, 3),
			TileType.EarlyStageRice => new(2, 3),
			TileType.EarlyStageCarrot => new(2, 3),
			TileType.EarlyStagePotato => new(2, 3),

			// mid stage seeds
			TileType.MidStageWheat => new(2, 3),
			TileType.MidStageRice => new(2, 3),
			TileType.MidStageCarrot => new(2, 3),
			TileType.MidStagePotato => new(2, 3),

			// late stage seeds
			TileType.LateStageWheat => new(2, 3),
			TileType.LateStageRice => new(2, 3),
			TileType.LateStageCarrot => new(2, 3),
			TileType.LateStagePotato => new(2, 3),

			// early stage trees
			TileType.EarlyStageAppleTree => new(2, 3),
			TileType.EarlyStageCherryTree => new(2, 3),
			TileType.EarlyStagePeachTree => new(2, 3),
			TileType.EarlyStagePearTree => new(2, 3),

			// mid stage trees
			TileType.MidStageAppleTree => new(2, 3),
			TileType.MidStageCherryTree => new(2, 3),
			TileType.MidStagePeachTree => new(2, 3),
			TileType.MidStagePearTree => new(2, 3),

			// late stage trees
			TileType.LateStageAppleTree => new(2, 3),
			TileType.LateStageCherryTree => new(2, 3),
			TileType.LateStagePeachTree => new(2, 3),
			TileType.LateStagePearTree => new(2, 3),
			_ => throw new UnreachableException()
		};
	}
}