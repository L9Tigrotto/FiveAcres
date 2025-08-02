

using Godot;
using System;
using System.Diagnostics;

public enum TileType
{
	Empty = -1,     // used to indicate an empty tile

	Grass = 0,      // hoe card can transform soil grass in soil
	Soil = 1,       // soil can accomodate plants and trees
	Stone = 2,      // stone can be removed with a pickaxe card
					//Water,

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

	Weed = 80,      // weed spreads to adjacent soil tiles and can "eat" plants and trees
}

public static class TileTypeExtensions
{
	public static Vector2I ToAtlasCoord(this TileType type, Random random)
	{
		return type switch
		{
			TileType.Grass => new(1, 0),
			TileType.Soil => new(2, 0),
			TileType.Stone => new(0, 9),
			//TileType.Water => new(2, 3),

			TileType.Weed => new(random.Next(0, 2 + 1), 2),

			// early stage seeds
			TileType.EarlyStageWheat => new(0, 1),
			TileType.EarlyStageRice => new(0, 3),
			TileType.EarlyStageCarrot => new(0, 4),
			TileType.EarlyStagePotato => new(0, 5),

			// mid stage seeds
			TileType.MidStageWheat => new(1, 1),
			TileType.MidStageRice => new(1, 3),
			TileType.MidStageCarrot => new(1, 4),
			TileType.MidStagePotato => new(1, 5),

			// late stage seeds
			TileType.LateStageWheat => new(2, 1),
			TileType.LateStageRice => new(2, 3),
			TileType.LateStageCarrot => new(2, 4),
			TileType.LateStagePotato => new(2, 5),

			// early stage trees
			TileType.EarlyStageAppleTree => new(0, 6),
			TileType.EarlyStageCherryTree => new(0, 6),
			TileType.EarlyStagePeachTree => new(0, 6),
			TileType.EarlyStagePearTree => new(0, 6),

			// mid stage trees
			TileType.MidStageAppleTree => new(1, 6),
			TileType.MidStageCherryTree => new(0, 7),
			TileType.MidStagePeachTree => new(1, 8),
			TileType.MidStagePearTree => new(2, 7),

			// late stage trees
			TileType.LateStageAppleTree => new(2, 6),
			TileType.LateStageCherryTree => new(1, 7),
			TileType.LateStagePeachTree => new(2, 8),
			TileType.LateStagePearTree => new(0, 8),
			_ => throw new UnreachableException()
		};
	}

	public static bool CanBeHarvested(this TileType type)
	{
		return type switch
		{
			TileType.LateStageWheat => true,
			TileType.LateStageRice => true,
			TileType.LateStageCarrot => true,
			TileType.LateStagePotato => true,
			TileType.LateStageAppleTree => true,
			TileType.LateStageCherryTree => true,
			TileType.LateStagePeachTree => true,
			TileType.LateStagePearTree => true,

			_ => false
		};
	}

	public static ItemType HarvestType(this TileType type)
	{
		return type switch
		{
			TileType.LateStageWheat => ItemType.Wheat,
			TileType.LateStageRice => ItemType.Rice,
			TileType.LateStageCarrot => ItemType.Carrot,
			TileType.LateStagePotato => ItemType.Potato,
			TileType.LateStageAppleTree => ItemType.Apple,
			TileType.LateStageCherryTree => ItemType.Cherry,
			TileType.LateStagePeachTree => ItemType.Peach,
			TileType.LateStagePearTree => ItemType.Pear,

			_ => throw new ArgumentException($"TileType {type} cannot be harvested.")
		};
	}

	public static bool IsReplaceableByWeed(this TileType type)
	{
		return type switch
		{
			TileType.Stone => false,
			TileType.Weed => false,
			_ => true
		};
	}
}