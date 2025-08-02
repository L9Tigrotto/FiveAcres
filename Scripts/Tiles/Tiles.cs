
using Godot;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

public static class Tiles
{
	public static readonly Dictionary<TileType, ITile> AllTiles = new()
	{
		[TileType.Empty] = new EmptyTile(),
		[TileType.Grass] = new GrassTile(),
		[TileType.Soil] = new SoilTile(),
		[TileType.Stone] = new StoneTile(),
		//[TileType.Water] = new WaterTile(),

		[TileType.Weed] = new WeedTile(),

		// early stage plants
		[TileType.EarlyStageWheat] = new EarlyStageWheatTile(),
		[TileType.EarlyStageRice] = new EarlyStageRiceTile(),
		[TileType.EarlyStageCarrot] = new EarlyStageCarrotTile(),
		[TileType.EarlyStagePotato] = new EarlyStagePotatoTile(),

		// mid stage plants
		[TileType.MidStageWheat] = new MidStageWheatTile(),
		[TileType.MidStageRice] = new MidStageRiceTile(),
		[TileType.MidStageCarrot] = new MidStageCarrotTile(),
		[TileType.MidStagePotato] = new MidStagePotatoTile(),

		// late stage plants
		[TileType.LateStageWheat] = new LateStageWheatTile(),
		[TileType.LateStageRice] = new LateStageRiceTile(),
		[TileType.LateStageCarrot] = new LateStageCarrotTile(),
		[TileType.LateStagePotato] = new LateStagePotatoTile(),

		// early stage trees
		[TileType.EarlyStageAppleTree] = new EarlyStageAppleTreeTile(),
		[TileType.EarlyStageCherryTree] = new EarlyStageCherryTreeTile(),
		[TileType.EarlyStagePeachTree] = new EarlyStagePeachTreeTile(),
		[TileType.EarlyStagePearTree] = new EarlyStagePearTreeTile(),

		// mid stage trees
		[TileType.MidStageAppleTree] = new MidStageAppleTreeTile(),
		[TileType.MidStageCherryTree] = new MidStageCherryTreeTile(),
		[TileType.MidStagePeachTree] = new MidStagePeachTreeTile(),
		[TileType.MidStagePearTree] = new MidStagePearTreeTile(),

		// late stage trees
		[TileType.LateStageAppleTree] = new LateStageAppleTreeTile(),
		[TileType.LateStageCherryTree] = new LateStageCherryTreeTile(),
		[TileType.LateStagePeachTree] = new LateStagePeachTreeTile(),
		[TileType.LateStagePearTree] = new LateStagePearTreeTile(),
	};

	public static readonly TileType[] EarlyStagePlantTypes =
	[
		TileType.EarlyStageWheat,
		TileType.EarlyStageRice,
		TileType.EarlyStageCarrot,
		TileType.EarlyStagePotato
	];

	public static readonly TileType[] MidStagePlantTypes =
	[
		TileType.MidStageWheat,
		TileType.MidStageRice,
		TileType.MidStageCarrot,
		TileType.MidStagePotato
	];

	public static readonly TileType[] LateStagePlantTypes =
	[
		TileType.LateStageWheat,
		TileType.LateStageRice,
		TileType.LateStageCarrot,
		TileType.LateStagePotato
	];

	public static readonly TileType[] EarlyStageTreeTypes =
	[
		TileType.EarlyStageAppleTree,
		TileType.EarlyStageCherryTree,
		TileType.EarlyStagePeachTree,
		TileType.EarlyStagePearTree
	];

	public static readonly TileType[] MidStageTreeTypes =
	[
		TileType.MidStageAppleTree,
		TileType.MidStageCherryTree,
		TileType.MidStagePeachTree,
		TileType.MidStagePearTree
	];

	public static readonly TileType[] LateStageTreeTypes =
	[
		TileType.LateStageAppleTree,
		TileType.LateStageCherryTree,
		TileType.LateStagePeachTree,
		TileType.LateStagePearTree
	];

	public static ITile Of(TileType type) { return AllTiles[type]; }
}

file class EmptyTile() : ITile
{
	public TileType Type { get; init; } = TileType.Empty;
	public int MinHarvest { get; init; } = 0;
	public int MaxHarvest { get; init; } = 0;
}

file class GrassTile() : ITile
{
	public TileType Type { get; init; } = TileType.Grass;
	public int MinHarvest { get; init; } = 0;
	public int MaxHarvest { get; init; } = 0;
}

file class SoilTile() : ITile
{
	public TileType Type { get; init; } = TileType.Soil;
	public int MinHarvest { get; init; } = 0;
	public int MaxHarvest { get; init; } = 0;
}

file class StoneTile() : ITile
{
	public TileType Type { get; init; } = TileType.Stone;
	public int MinHarvest { get; init; } = 0;
	public int MaxHarvest { get; init; } = 0;
}

file class WeedTile() : ITile
{
	public TileType Type { get; init; } = TileType.Weed;
	public int MinHarvest { get; init; } = 0;
	public int MaxHarvest { get; init; } = 0;
}

file class BaseGrowableTile(TileType thisType, TileType nextType, int minHarvest = 0, int maxHarvest = 0, float probabilityIncreaseOnFailure = 0.03f) : ITile
{
	public TileType Type { get { return ThisType; } }

	public TileType ThisType { get; init; } = thisType;
	public TileType NextType { get; init; } = nextType;
	public int MinHarvest { get; init; } = minHarvest;
	public int MaxHarvest { get; init; } = maxHarvest;
	public float ProbabilityIncreaseOnFailure { get; init; } = probabilityIncreaseOnFailure;

	public Dictionary<Vector2I, float> Data { get; } = [];

	public virtual void ResetData(Vector2I postion)
	{
		if (!Data.TryAdd(postion, 0.0f)) { Data[postion] = 0.0f; }
	}

	public void SimulateSecond(int second, Vector2I thisPostion, TileGrid grid)
	{
		if (ThisType.CanBeHarvested() || second == 0 || second % 2 != 0) { return; }

		float roll = grid.Random.NextSingle();
		float cellProbability = Data[thisPostion];
		if (roll <= cellProbability)
		{
			grid.SetCell(thisPostion, NextType);
			Data[thisPostion] = 0.0f; // reset the probability for this tile
		}
		else { Data[thisPostion] = cellProbability += ProbabilityIncreaseOnFailure; }
	}

	public virtual bool TryClick(Vector2I thisPostion, TileGrid grid, Storage storage)
	{
		if (!ThisType.CanBeHarvested()) { return false; }

		int generatedValue = grid.Random.Next(MinHarvest, MaxHarvest + 1);
		storage.AddItem(ThisType.HarvestType(), generatedValue);

		grid.SetCell(thisPostion, NextType);
		Data[thisPostion] = 0.0f; // reset the probability for this tile
		return true;
	}
}

file class BaseHarvestablePlantsTile : BaseGrowableTile
{
	public BaseHarvestablePlantsTile(TileType thisType, TileType nextType, float probabilityIncreaseOnFailure = 0.03f)
		: base(thisType, nextType, minHarvest: 1, maxHarvest: 5, probabilityIncreaseOnFailure)
	{
		if (!thisType.CanBeHarvested()) 
		{ 
			throw new ArgumentException(
				$"TileType {thisType} cannot be harvested, but BaseHarvestableTile requires it to be harvestable.");
		}
	}
}

file class BaseHarvestableTreeTile : BaseGrowableTile
{
	public BaseHarvestableTreeTile(TileType thisType, TileType nextType, float probabilityIncreaseOnFailure = 0.03f)
		: base(thisType, nextType, minHarvest: 2, maxHarvest: 10, probabilityIncreaseOnFailure)
	{
		if (!thisType.CanBeHarvested())
		{
			throw new ArgumentException(
				$"TileType {thisType} cannot be harvested, but BaseHarvestableTile requires it to be harvestable.");
		}
	}
}

// wheat stages
file class EarlyStageWheatTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageWheat, nextType: TileType.MidStageWheat);

file class MidStageWheatTile() : BaseGrowableTile(
	thisType: TileType.MidStageWheat, nextType: TileType.LateStageWheat);

file class LateStageWheatTile() : BaseHarvestablePlantsTile(
	thisType: TileType.LateStageWheat, nextType: TileType.EarlyStageWheat);


// rice stages
file class EarlyStageRiceTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageRice, nextType: TileType.MidStageRice);

file class MidStageRiceTile() : BaseGrowableTile(
	thisType: TileType.MidStageRice, nextType: TileType.LateStageRice);

file class LateStageRiceTile() : BaseHarvestablePlantsTile(
	thisType: TileType.LateStageRice, nextType: TileType.EarlyStageRice);


// carrot stages
file class EarlyStageCarrotTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageCarrot, nextType: TileType.MidStageCarrot);

file class MidStageCarrotTile() : BaseGrowableTile(
	thisType: TileType.MidStageCarrot, nextType: TileType.LateStageCarrot);

file class LateStageCarrotTile() : BaseHarvestablePlantsTile(
	thisType: TileType.LateStageCarrot, nextType: TileType.EarlyStageCarrot);


// potato stages
file class EarlyStagePotatoTile() : BaseGrowableTile(
	thisType: TileType.EarlyStagePotato, nextType: TileType.MidStagePotato);

file class MidStagePotatoTile() : BaseGrowableTile(
	thisType: TileType.MidStagePotato, nextType: TileType.LateStagePotato);

file class LateStagePotatoTile() : BaseHarvestablePlantsTile(
	thisType: TileType.LateStagePotato, nextType: TileType.EarlyStagePotato);


// apple stages
file class EarlyStageAppleTreeTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageAppleTree, nextType: TileType.MidStageAppleTree);

file class MidStageAppleTreeTile() : BaseGrowableTile(
	thisType: TileType.MidStageAppleTree, nextType: TileType.LateStageAppleTree);

file class LateStageAppleTreeTile() : BaseHarvestableTreeTile(
	thisType: TileType.LateStageAppleTree, nextType: TileType.MidStageAppleTree);


// cherry stages
file class EarlyStageCherryTreeTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageCherryTree, nextType: TileType.MidStageCherryTree);

file class MidStageCherryTreeTile() : BaseGrowableTile(
	thisType: TileType.MidStageCherryTree, nextType: TileType.LateStageCherryTree);

file class LateStageCherryTreeTile() : BaseHarvestableTreeTile(
	thisType: TileType.LateStageCherryTree, nextType: TileType.MidStageCherryTree);


// peach stages
file class EarlyStagePeachTreeTile() : BaseGrowableTile(
	thisType: TileType.EarlyStagePeachTree, nextType: TileType.MidStagePeachTree);

file class MidStagePeachTreeTile() : BaseGrowableTile(
	thisType: TileType.MidStagePeachTree, nextType: TileType.LateStagePeachTree);

file class LateStagePeachTreeTile() : BaseHarvestableTreeTile(
	thisType: TileType.LateStagePeachTree, nextType: TileType.MidStagePeachTree);


// pear stages
file class EarlyStagePearTreeTile() : BaseGrowableTile(
	thisType: TileType.EarlyStagePearTree, nextType: TileType.MidStagePearTree);

file class MidStagePearTreeTile() : BaseGrowableTile(
	thisType: TileType.MidStagePearTree, nextType: TileType.LateStagePearTree);

file class LateStagePearTreeTile() : BaseHarvestableTreeTile(
	thisType: TileType.LateStagePearTree, nextType: TileType.MidStagePearTree);