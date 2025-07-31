
using System.Collections.Generic;

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

	public static readonly TileType[] EarlyStageSeedTypes =
	[
		TileType.EarlyStageWheat,
		TileType.EarlyStageRice,
		TileType.EarlyStageCarrot,
		TileType.EarlyStagePotato
	];

	public static readonly TileType[] MidStageSeedTypes =
	[
		TileType.MidStageWheat,
		TileType.MidStageRice,
		TileType.MidStageCarrot,
		TileType.MidStagePotato
	];

	public static readonly TileType[] LateStageSeedTypes =
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

file class EarlyStageWheatTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStageWheat;
}

file class EarlyStageRiceTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStageRice;
}

file class EarlyStageCarrotTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStageCarrot;
}

file class EarlyStagePotatoTile() : ITile
{
	public TileType Type { get; } = TileType.EarlyStagePotato;
}

file class MidStageWheatTile() : ITile
{
	public TileType Type { get; } = TileType.MidStageWheat;
}

file class MidStageRiceTile() : ITile
{
	public TileType Type { get; } = TileType.MidStageRice;
}

file class MidStageCarrotTile() : ITile
{
	public TileType Type { get; } = TileType.MidStageCarrot;
}

file class MidStagePotatoTile() : ITile
{
	public TileType Type { get; } = TileType.MidStagePotato;
}

file class LateStageWheatTile() : ITile
{
	public TileType Type { get; } = TileType.LateStageWheat;
}

file class LateStageRiceTile() : ITile
{
	public TileType Type { get; } = TileType.LateStageRice;
}

file class LateStageCarrotTile() : ITile
{
	public TileType Type { get; } = TileType.LateStageCarrot;
}

file class LateStagePotatoTile() : ITile
{
	public TileType Type { get; } = TileType.LateStagePotato;
}

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