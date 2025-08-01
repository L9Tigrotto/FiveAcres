
using Godot;
using System;
using System.Collections.Generic;

public static class Cards
{
	public static readonly ICard[] AllCards = [
		// plants
		new PlantWheatCard(),
		new PlantRiceCard(),
		new PlantCarrotCard(),
		new PlantPotatoCard(),

		// trees
		new PlantAppleTreeCard(),
		new PlantCherryTreeCard(),
		new PlantPeachTreeCard(),
		new PlantPearTreeCard(),

		// weed
		new RemoveWeed1Card(),
		new RemoveWeed2Card(),
		new RemoveWeed3Card(),
	];
}

file class EmptyCard() : ICard
{
	public Texture2D Image { get; init; } // TODO load a default image
	public string Name { get; init; } = "";
	public string Description { get; init; } = "";

	public (ItemType Type, int Amount)[] Cost { get; init; } = [(ItemType.None, 9999)];

	public int Radius { get; init; } = 0;
}

file class DefaultReplaceTileCard(string imagePath, string name, string description, (ItemType Type, int Amount)[] cost,
	int radius, TileType replaceWith, Predicate<TileType> replacePredicate) : ICard
{
	public Texture2D Image { get; init; } // TODO load the image from the given path
	public string Name { get; init; } = name;
	public string Description { get; init; } = description;

	public (ItemType Type, int Amount)[] Cost { get; init; } = cost;

	public int Radius { get; init; } = radius;

	public void Use(List<Vector2I> areaOfEffect, TileGrid tileGrid)
	{
		foreach (Vector2I position in areaOfEffect)
		{
			TileType tileType = tileGrid.Grid[position.X, position.Y].Type;

			if (replacePredicate(tileType))
			{
				tileGrid.SetCell(position, replaceWith);
			}
		}
	}
}

// plants
file class PlantWheatCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Wheat", description: "",
	cost: [(ItemType.Potato, 15)],
	radius: 1,
	replaceWith: TileType.EarlyStageWheat, (tileType) => { return tileType == TileType.Soil; });

file class PlantRiceCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Rice", description: "",
	cost: [(ItemType.Carrot, 15)],
	radius: 1,
	replaceWith: TileType.EarlyStageRice, (tileType) => { return tileType == TileType.Soil; });

file class PlantCarrotCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Carrot", description: "",
	cost: [(ItemType.Wheat, 15)], 
	radius: 1,
	replaceWith: TileType.EarlyStageCarrot, (tileType) => { return tileType == TileType.Soil; });

file class PlantPotatoCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Potato", description: "",
	cost: [(ItemType.Rice, 15)], 
	radius: 1,
	replaceWith: TileType.EarlyStagePotato, (tileType) => { return tileType == TileType.Soil; });

// trees
file class PlantAppleTreeCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Apple Tree", description: "",
	cost: [(ItemType.Peach, 50)],
	radius: 1,
	replaceWith: TileType.EarlyStageAppleTree, (tileType) => { return tileType == TileType.Soil; });

file class PlantCherryTreeCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Apple Cherry", description: "",
	cost: [(ItemType.Pear, 50)], 
	radius: 1,
	replaceWith: TileType.EarlyStageCherryTree, (tileType) => { return tileType == TileType.Soil; });

file class PlantPeachTreeCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Apple Peach", description: "",
	cost: [(ItemType.Cherry, 50)], 
	radius: 1,
	replaceWith: TileType.EarlyStagePeachTree, (tileType) => { return tileType == TileType.Soil; });

file class PlantPearTreeCard() : DefaultReplaceTileCard(
	imagePath: "",
	name: "Plant Apple Pear", description: "",
	cost: [(ItemType.Apple, 50)], 
	radius: 1,
	replaceWith: TileType.EarlyStagePotato, (tileType) => { return tileType == TileType.Soil; });


file class Cost2GeneratorCard(string imagePath, string name, string description, int amount1, int amount2,
	int radius, TileType replaceWith, Predicate<TileType> replacePredicate) : DefaultReplaceTileCard(
	imagePath,
	name, description,
	cost: [((ItemType)Random.Shared.Next(1, 8 + 1), amount1), ((ItemType)Random.Shared.Next(1, 8 + 1), amount2)],
	radius,
	replaceWith, replacePredicate);

// weed
file class RemoveWeed1Card() : Cost2GeneratorCard(
	imagePath: "",
	name: "Plant Apple Pear", description: "",
	amount1: 20, amount2: 5,
	radius: 1,
	replaceWith: TileType.EarlyStagePotato, (tileType) => { return tileType == TileType.Weed; });

file class RemoveWeed2Card() : Cost2GeneratorCard(
	imagePath: "",
	name: "Plant Apple Pear", description: "",
	amount1: 30, amount2: 10,
	radius: 2,
	replaceWith: TileType.EarlyStagePotato, (tileType) => { return tileType == TileType.Weed; });

file class RemoveWeed3Card() : Cost2GeneratorCard(
	imagePath: "",
	name: "Plant Apple Pear", description: "",
	amount1: 40, amount2: 20,
	radius: 3,
	replaceWith: TileType.EarlyStagePotato, (tileType) => { return tileType == TileType.Weed; });

// remove stone

// fertilize (grass -> soil)