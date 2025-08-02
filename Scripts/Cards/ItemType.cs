
using System;

public enum ItemType
{
	None,

	Wheat,
	Rice,
	Carrot,
	Potato,

	Apple,
	Cherry,
	Peach,
	Pear,
}

public static class ItemTypeExtensions
{
	public static string ResourceLocation(this ItemType itemType)
	{
		return itemType switch
		{
			ItemType.Wheat => "res://Resources/card_arts/wheat.png",
			ItemType.Rice => "res://Resources/card_arts/rice.png",
			ItemType.Carrot => "res://Resources/card_arts/carrot.png",
			ItemType.Potato => "res://Resources/card_arts/potato.png",
			ItemType.Apple => "res://Resources/card_arts/apple.png",
			ItemType.Cherry => "res://Resources/card_arts/cherry.png",
			ItemType.Peach => "res://Resources/card_arts/peach.png",
			ItemType.Pear => "res://Resources/card_arts/pear.png",
			_ => throw new ArgumentException($"Unknown ItemType: {itemType}")
		};
	}
}

