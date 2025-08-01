using Godot;
using System;
using System.Diagnostics;

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

		// update text labels with initial counts
		WheatLabel.Text = $"{WheatCount}";
		RiceLabel.Text = $"{RiceCount}";
		CarrotLabel.Text = $"{CarrotCount}";
		PotatoLabel.Text = $"{PatatoCount}";

		AppleLabel.Text = $"{AppleCount}";
		CherryLabel.Text = $"{CherryCount}";
		PeachLabel.Text = $"{PeachCount}";
		PearLabel.Text = $"{PearCount}";
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
		WheatLabel.Text = $"{WheatCount}";
	}

	public void AddRice(int amount)
	{
		RiceCount += amount;
		RiceLabel.Text = $"{RiceCount}";
	}

	public void AddCarrot(int amount)
	{
		CarrotCount += amount;
		CarrotLabel.Text = $"{CarrotCount}";
	}

	public void AddPotato(int amount)
	{
		PatatoCount += amount;
		PotatoLabel.Text = $"{PatatoCount}";
	}

	public void AddApple(int amount)
	{
		AppleCount += amount;
		AppleLabel.Text = $"{AppleCount}";
	}

	public void AddCherry(int amount)
	{
		CherryCount += amount;
		CherryLabel.Text = $"{CherryCount}";
	}

	public void AddPeach(int amount)
	{
		PeachCount += amount;
		PeachLabel.Text = $"{PeachCount}";
	}

	public void AddPear(int amount)
	{
		PearCount += amount;
		PearLabel.Text = $"{PearCount}";
	}
}
