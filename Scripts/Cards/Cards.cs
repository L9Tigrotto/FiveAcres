
using Godot;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
		new RemoveWeed2Card(),
		new RemoveWeed3Card(),
		new RemoveWeed4Card(),
	];

	public static ICard GetSpecificOrRandomCard(string name)
	{
		int index = -1;
		for (int i = 0; i < AllCards.Length; i++)
		{
			if (AllCards[i].Name == name) { index = i; break; }
		}

		if (index == -1) { index = Random.Shared.Next(AllCards.Length); }

		return AllCards[index];
	}

	public static ICard GetRandomCard()
	{
		int index = Random.Shared.Next(AllCards.Length);

		GD.Print($"Card found at index {index}");
		return AllCards[index];
	}
}

file class EmptyCard() : ICard
{
	public Texture2D Image { get; init; } = GD.Load<Texture2D>("res://icon.svg");
	public string Name { get; init; } = "";
	public string Description { get; init; } = "";

	public (ItemType Type, int Amount)[] StoreCost { get; init; } = [(ItemType.None, 9999)];
	public int PlayCost { get; init; } = 99999;

	public int Radius { get; init; } = 0;
}

file class DefaultReplaceTileCard(string imagePath, string name, string description, (ItemType Type, int Amount)[] storeCost, int playCost,
	int radius, TileType replaceWith, Predicate<TileType> replacePredicate) : ICard
{
	public Texture2D Image { get; init; } = GD.Load<Texture2D>(imagePath);
	public string Name { get; init; } = name;
	public string Description { get; init; } = description;

	public (ItemType Type, int Amount)[] StoreCost { get; init; } = storeCost;
	public int PlayCost { get; init; } = playCost;

	public int Radius { get; init; } = radius;

	public bool Use(List<Vector2I> areaOfEffect, TileGrid tileGrid)
	{
		int affected = 0;
		foreach (Vector2I position in areaOfEffect)
		{
			TileType tileType = tileGrid.Grid[position.X, position.Y].Type;

			if (replacePredicate(tileType) || tileType == TileType.Grass)
			{
				affected++;
				tileGrid.SetCell(position, replaceWith);
			}
		}

		return Radius > 1 || affected > 0;
	}
}

// plants
file class PlantWheatCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/wheat.png",
	name: "Wheat Crop", description: "Plant a wheat crop on soil. \n-Grows at normal rate \n-Small yield",
	storeCost: [(ItemType.Potato, 15)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStageWheat, (tileType) => { return tileType == TileType.Soil; });

file class PlantRiceCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/rice.png",
	name: "Rice Crop", description: "Plant a rice crop on soil. \n-Grows very fast \n-Very small yield",
	storeCost: [(ItemType.Carrot, 15)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStageRice, (tileType) => { return tileType == TileType.Soil; });

file class PlantCarrotCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/carrot.png",
	name: "Plant Carrot", description: "Plant a carrot on soil. \n-Grows very slowly \n-Very big yield",
	storeCost: [(ItemType.Wheat, 15)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStageCarrot, (tileType) => { return tileType == TileType.Soil; });

file class PlantPotatoCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/potato.png",
	name: "Plant Potato", description: "Plant a potato on soil. \n-Grows slowly \n-Big yield",
	storeCost: [(ItemType.Rice, 15)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStagePotato, (tileType) => { return tileType == TileType.Soil; });

// trees
file class PlantAppleTreeCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/apple.png",
	name: "Apple Tree", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Peach, 50)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStageAppleTree, (tileType) => { return tileType == TileType.Soil; });

file class PlantCherryTreeCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/cherry.png",
	name: "Apple Cherry", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Pear, 50)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStageCherryTree, (tileType) => { return tileType == TileType.Soil; });

file class PlantPeachTreeCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/peach.png",
	name: "Apple Peach", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Cherry, 50)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStagePeachTree, (tileType) => { return tileType == TileType.Soil; });

file class PlantPearTreeCard() : DefaultReplaceTileCard(
	imagePath: "res://Resources/card_arts/pear.png",
	name: "Apple Pear", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Apple, 50)], playCost: 5,
	radius: 1,
	replaceWith: TileType.EarlyStagePearTree, (tileType) => { return tileType == TileType.Soil; });


file class Cost2GeneratorCard(string imagePath, string name, string description, int amount1, int amount2, int playCost,
	int radius, TileType replaceWith, Predicate<TileType> replacePredicate) : DefaultReplaceTileCard(
	imagePath,
	name, description,
	storeCost: [((ItemType)Random.Shared.Next(1, 8 + 1), amount1), ((ItemType)Random.Shared.Next(1, 8 + 1), amount2)],
	playCost, radius,
	replaceWith, replacePredicate);

// weed
file class RemoveWeed2Card() : Cost2GeneratorCard(
	imagePath: "res://Resources/card_arts/scissor.png",
	name: "Weed Manicure", description: "Destroys all weeds in a 2 tiles radius.",
	amount1: 20, amount2: 5, playCost: 5,
	radius: 1,
	replaceWith: TileType.Grass, (tileType) => tileType == TileType.Weed);

file class RemoveWeed3Card() : Cost2GeneratorCard(
	imagePath: "res://Resources/card_arts/scissor.png",
	name: "Weed Reaper", description: "Destroys all weeds in a 3 tiles radius.",
	amount1: 30, amount2: 10, playCost: 5,
	radius: 2,
	replaceWith: TileType.Grass, (tileType) => tileType == TileType.Weed);

file class RemoveWeed4Card() : Cost2GeneratorCard(
	imagePath: "res://Resources/card_arts/explosiveBarrel.png",
	name: "Weed Nuker", description: "Destroys all weeds in a 4 tiles radius.",
	amount1: 40, amount2: 20, playCost: 5,
	radius: 3,
	replaceWith: TileType.Grass, (tileType) => tileType == TileType.Weed);

// remove stone

// fertilize (grass -> soil)