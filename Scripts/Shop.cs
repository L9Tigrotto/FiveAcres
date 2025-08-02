using Godot;
using System;

public partial class Shop : Control
{
	public bool IsOpen { get; private set; }

	private Button OpenShopButton { get; set; }
	private Button CloseShopButton { get; set; }
	private TextureRect Background { get; set; }

	private Card Card1 { get; set; }
	private Card Card2 { get; set; }
	private Card Card3 { get; set; }

	public bool IsCard1Available { get; set; }
	public bool IsCard2Available { get; set; }
	public bool IsCard3Available { get; set; }

	public override void _Ready()
	{
		IsOpen = false;

		OpenShopButton = GetNode<Button>("OpenShopButton");
		CloseShopButton = GetNode<Button>("CloseShopButton");
		Background = GetNode<TextureRect>("Background");

		Card1 = GetNode<Card>("Card1");
		Card2 = GetNode<Card>("Card2");
		Card3 = GetNode<Card>("Card3");

		OpenShopButton.Visible = !IsOpen;
		CloseShopButton.Visible = IsOpen;
		Background.Visible = IsOpen;
		Refill();
		Card1.Visible = false;
		Card2.Visible = false;
		Card3.Visible = false;
	}

	public void Open()
	{
		IsOpen = true;
		OpenShopButton.Visible = !IsOpen;
		CloseShopButton.Visible = IsOpen;
		Background.Visible = IsOpen;

		Card1.Visible = IsCard1Available;
		Card2.Visible = IsCard2Available;
		Card3.Visible = IsCard3Available;
	}

	public void Close()
	{
		IsOpen = false;
		OpenShopButton.Visible = !IsOpen;
		CloseShopButton.Visible = IsOpen;
		Background.Visible = IsOpen;

		Card1.Visible = false;
		Card2.Visible = false;
		Card3.Visible = false;
	}

	public void Refill()
	{
		Card1.SetCard(Cards.GetRandomCard());
		Card2.SetCard(Cards.GetRandomCard());
		Card3.SetCard(Cards.GetRandomCard());

		IsCard1Available = true;
		IsCard2Available = true;
		IsCard3Available = true;
	}
}
