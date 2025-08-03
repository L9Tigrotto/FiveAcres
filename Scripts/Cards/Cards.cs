
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
		
		// utilities
		new PickaxeCard(),
	];

	public static ICard GedSpecificOrRandom(string name)
	{
		int index = -1;
		for (int i = 0; i < AllCards.Length; i++)
		{
			if (AllCards[i].Name == name) { index = i; break; }
		}

		if (index == -1) { index = Random.Shared.Next(AllCards.Length); }

		return AllCards[index];
	}

	public static ICard GetRandomPlant()
	{
		int index = Random.Shared.Next(0, 3 + 1);
		return AllCards[index];
	}

	public static ICard GetRandomTree()
	{
		int index = Random.Shared.Next(4, 7 + 1);
		return AllCards[index];
	}

	public static ICard GetRandomCard()
	{
		int index = Random.Shared.Next(AllCards.Length);
		return AllCards[index];
	}
}

file class DefaultReplaceTileCard(CardType type, string imagePath, string name, string description, (ItemType Type, int Amount)[] storeCost, int playCost,
	int radius, TileType replaceWith, Predicate<TileType> replacePredicate) : ICard
{
	public CardType Type { get; init; } = type;

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

			if (replacePredicate(tileType))
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
	type: CardType.Plant,
	imagePath: ItemType.Wheat.ResourceLocation(),
	name: "Wheat Crop", description: "Plant a wheat crop. \n-Grows at normal rate \n-Small yield",
	storeCost: [(ItemType.Potato, 8)], playCost: 25,
	radius: 1,
	replaceWith: TileType.EarlyStageWheat, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });

file class PlantRiceCard() : DefaultReplaceTileCard(
	type: CardType.Plant,
	imagePath: ItemType.Rice.ResourceLocation(),
	name: "Rice Crop", description: "Plant a rice crop. \n-Grows very fast \n-Very small yield",
	storeCost: [(ItemType.Carrot, 8)], playCost: 25,
	radius: 1,
	replaceWith: TileType.EarlyStageRice, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });

file class PlantCarrotCard() : DefaultReplaceTileCard(
	type: CardType.Plant,
	imagePath: ItemType.Carrot.ResourceLocation(),
	name: "Plant Carrot", description: "Plant a carrot. \n-Grows very slowly \n-Very big yield",
	storeCost: [(ItemType.Wheat, 8)], playCost: 25,
	radius: 1,
	replaceWith: TileType.EarlyStageCarrot, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });

file class PlantPotatoCard() : DefaultReplaceTileCard(
	type: CardType.Plant,
	imagePath: ItemType.Potato.ResourceLocation(),
	name: "Plant Potato", description: "Plant a potato. \n-Grows slowly \n-Big yield",
	storeCost: [(ItemType.Rice, 8)], playCost: 25,
	radius: 1,
	replaceWith: TileType.EarlyStagePotato, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });

// trees
file class PlantAppleTreeCard() : DefaultReplaceTileCard(
	type: CardType.Tree,
	imagePath: ItemType.Apple.ResourceLocation(),
	name: "Apple Tree", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Peach, 25)], playCost: 60,
	radius: 1,
	replaceWith: TileType.EarlyStageAppleTree, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });

file class PlantCherryTreeCard() : DefaultReplaceTileCard(
	type: CardType.Tree,
	imagePath: ItemType.Cherry.ResourceLocation(),
	name: "Cherry Tree", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Pear, 25)], playCost: 60,
	radius: 1,
	replaceWith: TileType.EarlyStageCherryTree, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });

file class PlantPeachTreeCard() : DefaultReplaceTileCard(
	type: CardType.Tree,
	imagePath: ItemType.Peach.ResourceLocation(),
	name: "Peach Tree", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Cherry, 25)], playCost: 60,
	radius: 1,
	replaceWith: TileType.EarlyStagePeachTree, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });

file class PlantPearTreeCard() : DefaultReplaceTileCard(
	type: CardType.Tree,
	imagePath: ItemType.Pear.ResourceLocation(),
	name: "Pear Tree", description: "It will take at least a generation to grow. Will it be worth?",
	storeCost: [(ItemType.Apple, 25)], playCost: 60,
	radius: 1,
	replaceWith: TileType.EarlyStagePearTree, (tileType) => { return tileType == TileType.Soil || tileType == TileType.Grass; });


file class Cost2GeneratorCard(CardType type, string imagePath, string name, string description, int amount1, int amount2, int playCost,
	int radius, TileType replaceWith, Predicate<TileType> replacePredicate) : DefaultReplaceTileCard(
	type, imagePath,
	name, description,
	storeCost: [((ItemType)Random.Shared.Next(1, 8 + 1), amount1), ((ItemType)Random.Shared.Next(1, 8 + 1), amount2)],
	playCost, radius,
	replaceWith, replacePredicate);

// weed
file class RemoveWeed1Card() : Cost2GeneratorCard(
	type: CardType.Cut,
	imagePath: "res://Resources/card_arts/scissor.png",
	name: "Weed Manicure", description: "Destroys all weeds in a 2 tiles radius.",
	amount1: 12, amount2: 3, playCost: 5,
	radius: 1,
	replaceWith: TileType.Grass, (tileType) => tileType == TileType.Weed);

file class RemoveWeed2Card() : Cost2GeneratorCard(
	type: CardType.Cut,
	imagePath: "res://Resources/card_arts/scissor.png",
	name: "Weed Reaper", description: "Destroys all weeds in a 3 tiles radius.",
	amount1: 20, amount2: 3, playCost: 10,
	radius: 2,
	replaceWith: TileType.Grass, (tileType) => tileType == TileType.Weed);

file class RemoveWeed3Card() : Cost2GeneratorCard(
	type: CardType.Cut,
	imagePath: "res://Resources/card_arts/explosiveBarrel.png",
	name: "Weed Nuker", description: "Destroys all weeds in a 4 tiles radius.",
	amount1: 30, amount2: 3, playCost: 20,
	radius: 3,
	replaceWith: TileType.Grass, (tileType) => tileType == TileType.Weed);

// remove stone
file class PickaxeCard() : DefaultReplaceTileCard(
	type: CardType.Pickaxe,
	imagePath: "res://Resources/card_arts/pickaxe.png",
	name: "Pickaxe", description: "Destroys 1 stone.",
	storeCost: [((ItemType)Random.Shared.Next(1, 8 + 1), 15)], playCost: 10,
	radius: 1,
	replaceWith: TileType.Grass, (tileType) => tileType == TileType.Stone);