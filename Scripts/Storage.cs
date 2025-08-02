using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Storage : Node2D
{
	public Dictionary<ItemType, int> Count { get; private set; }
	
	private Label WheatLabel { get; set; }
	private Label RiceLabel { get; set; }
	private Label CarrotLabel { get; set; }
	private Label PotatoLabel { get; set; }

	private Label AppleLabel { get; set; }
	private Label CherryLabel { get; set; }
	private Label PeachLabel { get; set; }
	private Label PearLabel { get; set; }

	public override void _Ready()
	{
		WheatLabel = GetNode<Label>("WheatLabel");
		RiceLabel = GetNode<Label>("RiceLabel");
		CarrotLabel = GetNode<Label>("CarrotLabel");
		PotatoLabel = GetNode<Label>("PotatoLabel");

		AppleLabel = GetNode<Label>("AppleLabel");
		CherryLabel = GetNode<Label>("CherryLabel");
		PeachLabel = GetNode<Label>("PeachLabel");
		PearLabel = GetNode<Label>("PearLabel");
		
		
		Count = new()
		{
			{ ItemType.Wheat, 0 },
			{ ItemType.Rice, 0 },
			{ ItemType.Carrot, 0 },
			{ ItemType.Potato, 0 },
		
			{ ItemType.Apple, 0 },
			{ ItemType.Cherry, 0 },
			{ ItemType.Peach, 0 },
			{ ItemType.Pear, 0 },
		};

		// update text labels with initial counts
		WheatLabel.Text = $"{Count[ItemType.Wheat]}";
		RiceLabel.Text = $"{Count[ItemType.Rice]}";
		CarrotLabel.Text = $"{Count[ItemType.Carrot]}";
		PotatoLabel.Text = $"{Count[ItemType.Potato]}";

		AppleLabel.Text = $"{Count[ItemType.Apple]}";
		CherryLabel.Text = $"{Count[ItemType.Cherry]}";
		PeachLabel.Text = $"{Count[ItemType.Peach]}";
		PearLabel.Text = $"{Count[ItemType.Pear]}";
	}

	public void AddItem(ItemType itemType, int amount)
	{
		switch (itemType)
		{
			case ItemType.None: return;

			case ItemType.Wheat: AddWheat(amount); break;
			case ItemType.Rice: AddRice(amount); break;
			case ItemType.Carrot: AddCarrot(amount); break;
			case ItemType.Potato: AddPotato(amount); break;

			case ItemType.Apple: AddApple(amount); break;
			case ItemType.Cherry: AddCherry(amount); break;
			case ItemType.Peach: AddPeach(amount); break;
			case ItemType.Pear: AddPear(amount); break;
			default: throw new UnreachableException($"Unknown item: {itemType}");
		}
	}

	public void AddWheat(int amount)
	{
		Count[ItemType.Wheat] += amount;
		WheatLabel.Text = $"{Count[ItemType.Wheat]}";
	}

	public void AddRice(int amount)
	{
		Count[ItemType.Rice] += amount;
		RiceLabel.Text = $"{Count[ItemType.Rice]}";
	}

	public void AddCarrot(int amount)
	{
		Count[ItemType.Carrot] += amount;
		CarrotLabel.Text = $"{Count[ItemType.Carrot]}";
	}

	public void AddPotato(int amount)
	{
		Count[ItemType.Potato] += amount;
		PotatoLabel.Text = $"{Count[ItemType.Potato]}";
	}

	public void AddApple(int amount)
	{
		Count[ItemType.Apple] += amount;
		AppleLabel.Text = $"{Count[ItemType.Apple]}";
	}

	public void AddCherry(int amount)
	{
		Count[ItemType.Cherry] += amount;
		CherryLabel.Text = $"{Count[ItemType.Cherry]}";
	}

	public void AddPeach(int amount)
	{
		Count[ItemType.Peach] += amount;
		PeachLabel.Text = $"{Count[ItemType.Peach]}";
	}

	public void AddPear(int amount)
	{
		Count[ItemType.Pear] += amount;
		PearLabel.Text = $"{Count[ItemType.Pear]}";
	}
}
