
using Godot;
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
	public TileType Type { get; } = TileType.Empty;
}

file class GrassTile() : ITile
{
	public TileType Type { get; } = TileType.Grass;
}

file class SoilTile() : ITile
{
	public TileType Type { get; } = TileType.Soil;
}

file class StoneTile() : ITile
{
	public TileType Type { get; } = TileType.Stone;
}

file class WeedTile() : ITile
{
	public TileType Type { get; } = TileType.Weed;
}

file class BaseGrowableTile : ITile
{
	public TileType Type { get { return ThisType; } }

	public TileType ThisType { get; init; }
	public TileType NextType { get; init; }
	public bool IsFullGrown { get; init; }
	public float IncreaseOnFailure { get; init; }

	public Dictionary<Vector2I, float> Data { get; } = [];

	public BaseGrowableTile(TileType thisType, TileType nextType, float increaseOnFailure = 0.03f, bool isFullGrown = false)
	{
		ThisType = thisType;
		NextType = nextType;
		IsFullGrown = isFullGrown;
		IncreaseOnFailure = increaseOnFailure;
	}

	public virtual void ResetData(Vector2I postion)
	{
		if (!Data.TryAdd(postion, 0.0f)) { Data[postion] = 0.0f; }
	}

	public void SimulateSecond(int second, Vector2I thisPostion, TileGrid grid)
	{
		if (IsFullGrown || second == 0 || second % 2 != 0) { return; }

		float roll = grid.Random.NextSingle();
		float cellProbability = Data[thisPostion];
		if (roll <= cellProbability)
		{
			grid.SetCell(thisPostion, NextType);
			Data[thisPostion] = 0.0f; // reset the probability for this tile
		}
		else { Data[thisPostion] = cellProbability += IncreaseOnFailure; }
	}

	public virtual void Click(Vector2I thisPostion, TileGrid grid, Storage storage)
	{
		int generatedValue = grid.Random.Next(1, 5);

		switch (ThisType)
		{
			case TileType.LateStageWheat: storage.AddWheat(generatedValue); break;
			case TileType.LateStageRice: storage.AddRice(generatedValue); break;
			case TileType.LateStageCarrot: storage.AddCarrot(generatedValue); break;
			case TileType.LateStagePotato: storage.AddPotato(generatedValue); break;

			case TileType.LateStageAppleTree: storage.AddApple(generatedValue); break;
			case TileType.LateStageCherryTree: storage.AddCherry(generatedValue); break;
			case TileType.LateStagePeachTree: storage.AddPeach(generatedValue); break;
			case TileType.LateStagePearTree: storage.AddPear(generatedValue); break;
		}

		grid.SetCell(thisPostion, NextType);
		Data[thisPostion] = 0.0f; // reset the probability for this tile
	}
}

// wheat stages
file class EarlyStageWheatTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageWheat, nextType: TileType.MidStageWheat)
{ }

file class MidStageWheatTile() : BaseGrowableTile(
	thisType: TileType.MidStageWheat, nextType: TileType.LateStageWheat)
{ }

file class LateStageWheatTile() : BaseGrowableTile(thisType: TileType.LateStageWheat, TileType.EarlyStageWheat, isFullGrown: true) { }


// rice stages
file class EarlyStageRiceTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageRice, nextType: TileType.MidStageRice)
{ }

file class MidStageRiceTile() : BaseGrowableTile(
	thisType: TileType.MidStageRice, nextType: TileType.LateStageRice)
{ }

file class LateStageRiceTile() : BaseGrowableTile(
	thisType: TileType.LateStageRice, TileType.EarlyStageRice)
{ }


// carrot stages
file class EarlyStageCarrotTile() : BaseGrowableTile(
	thisType: TileType.EarlyStageCarrot, nextType: TileType.MidStageCarrot)
{ }

file class MidStageCarrotTile() : BaseGrowableTile(
	thisType: TileType.MidStageCarrot, nextType: TileType.LateStageCarrot)
{ }

file class LateStageCarrotTile() : BaseGrowableTile(
	thisType: TileType.LateStageCarrot, TileType.EarlyStageCarrot)
{ }


// potato stages
file class EarlyStagePotatoTile() : BaseGrowableTile(
	thisType: TileType.EarlyStagePotato, nextType: TileType.MidStagePotato)
{ }

file class MidStagePotatoTile() : BaseGrowableTile(
	thisType: TileType.MidStagePotato, nextType: TileType.LateStagePotato)
{ }

file class LateStagePotatoTile() : BaseGrowableTile(
	thisType: TileType.LateStagePotato, TileType.EarlyStagePotato)
{ }






file class EarlyStageAppleTreeTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStageAppleTree;
}

file class EarlyStageCherryTreeTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStageCherryTree;
}

file class EarlyStagePeachTreeTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStagePeachTree;
}

file class EarlyStagePearTreeTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStagePearTree;
}

file class MidStageAppleTreeTile() : ITile
{
	public TileType Type { get; } = TileType.MidStageAppleTree;
}

file class MidStageCherryTreeTile() : ITile
{
	public TileType Type { get; } = TileType.MidStageCherryTree;
}

file class MidStagePeachTreeTile() : ITile
{
	public TileType Type { get; } = TileType.MidStagePeachTree;
}

file class MidStagePearTreeTile() : ITile
{
	public TileType Type { get; } = TileType.MidStagePearTree;
}

file class LateStageAppleTreeTile() : ITile
{
	public TileType Type { get; } = TileType.LateStageAppleTree;
}

file class LateStageCherryTreeTile() : ITile
{
	public TileType Type { get; } = TileType.LateStageCherryTree;
}

file class LateStagePeachTreeTile() : ITile
{
	public TileType Type { get; } = TileType.LateStagePeachTree;
}

file class LateStagePearTreeTile() : ITile
{
	public TileType Type { get; } = TileType.LateStagePearTree;
}