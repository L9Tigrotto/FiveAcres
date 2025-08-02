using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Storage : Node2D
{
	public int WheatCount { get; private set; }
	public int RiceCount { get; private set; }
	public int CarrotCount { get; private set; }
	public int PatatoCount { get; private set; }

	public int AppleCount { get; private set; }
	public int CherryCount { get; private set; }
	public int PeachCount { get; private set; }
	public int PearCount { get; private set; }

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
		WheatCount = 0;
		RiceCount = 0;
		CarrotCount = 0;
		PatatoCount = 0;

		AppleCount = 0;
		CherryCount = 0;
		PeachCount = 0;
		PearCount = 0;

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
		//Debug additions
		Count[ItemType.Wheat] = 999;
		Count[ItemType.Rice] = 999;
		Count[ItemType.Carrot] = 999;
		Count[ItemType.Potato] = 999;
		Count[ItemType.Apple] = 999;
		Count[ItemType.Cherry] = 999;
		Count[ItemType.Peach] = 999;
		Count[ItemType.Apple] = 999;
		Count[ItemType.Pear] = 999;
		

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
		WheatCount += amount;
		
		Count[ItemType.Wheat] += amount;
		WheatLabel.Text = $"{Count[ItemType.Wheat]}";
	}

	public void AddRice(int amount)
	{
		RiceCount += amount;
		
		Count[ItemType.Rice] += amount;
		RiceLabel.Text = $"{Count[ItemType.Rice]}";
	}

	public void AddCarrot(int amount)
	{
		CarrotCount += amount;
		
		Count[ItemType.Carrot] += amount;
		CarrotLabel.Text = $"{Count[ItemType.Carrot]}";
	}

	public void AddPotato(int amount)
	{
		PatatoCount += amount;
		
		Count[ItemType.Potato] += amount;
		PotatoLabel.Text = $"{Count[ItemType.Potato]}";
	}

	public void AddApple(int amount)
	{
		AppleCount += amount;
		
		Count[ItemType.Apple] += amount;
		AppleLabel.Text = $"{Count[ItemType.Apple]}";
	}

	public void AddCherry(int amount)
	{
		CherryCount += amount;
		
		Count[ItemType.Cherry] += amount;
		CherryLabel.Text = $"{Count[ItemType.Cherry]}";
	}

	public void AddPeach(int amount)
	{
		PeachCount += amount;
		
		Count[ItemType.Peach] += amount;
		PeachLabel.Text = $"{Count[ItemType.Peach]}";
	}

	public void AddPear(int amount)
	{
		PearCount += amount;
		
		Count[ItemType.Pear] += amount;
		PearLabel.Text = $"{Count[ItemType.Pear]}";
	}
}
