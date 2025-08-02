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

	private bool IsCard1Available { get; set; }
	private bool IsCard2Available { get; set; }
	private bool IsCard3Available { get; set; }

	private Sprite2D Card1Cost1Image { get; set; }
	private Sprite2D Card1Cost2Image { get; set; }
	private Sprite2D Card2Cost1Image { get; set; }
	private Sprite2D Card2Cost2Image { get; set; }
	private Sprite2D Card3Cost1Image { get; set; }
	private Sprite2D Card3Cost2Image { get; set; }

	private Label Card1Cost1Label { get; set; }
	private Label Card1Cost2Label { get; set; }
	private Label Card2Cost1Label { get; set; }
	private Label Card2Cost2Label { get; set; }
	private Label Card3Cost1Label { get; set; }
	private Label Card3Cost2Label { get; set; }

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

		Card1.Visible = false;
		Card2.Visible = false;
		Card3.Visible = false;

		Card1Cost1Image = GetNode<Sprite2D>("Card1Cost1Image");
		Card1Cost2Image = GetNode<Sprite2D>("Card1Cost2Image");
		Card2Cost1Image = GetNode<Sprite2D>("Card2Cost1Image");
		Card2Cost2Image = GetNode<Sprite2D>("Card2Cost2Image");
		Card3Cost1Image = GetNode<Sprite2D>("Card3Cost1Image");
		Card3Cost2Image = GetNode<Sprite2D>("Card3Cost2Image");

		Card1Cost1Image.Visible = false;
		Card1Cost2Image.Visible = false;
		Card2Cost1Image.Visible = false;
		Card2Cost2Image.Visible = false;
		Card3Cost1Image.Visible = false;
		Card3Cost2Image.Visible = false;

		Card1Cost1Label = GetNode<Label>("Card1Cost1Label");
		Card1Cost2Label = GetNode<Label>("Card1Cost2Label");
		Card2Cost1Label = GetNode<Label>("Card2Cost1Label");
		Card2Cost2Label = GetNode<Label>("Card2Cost2Label");
		Card3Cost1Label = GetNode<Label>("Card3Cost1Label");
		Card3Cost2Label = GetNode<Label>("Card3Cost2Label");

		Card1Cost1Label.Visible = false;
		Card1Cost2Label.Visible = false;
		Card2Cost1Label.Visible = false;
		Card2Cost2Label.Visible = false;
		Card3Cost1Label.Visible = false;
		Card3Cost2Label.Visible = false;

		Refill();
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

		Card1Cost1Image.Visible = IsCard1Available && Card1.CardInfo.StoreCost.Length > 0;
		Card1Cost2Image.Visible = IsCard1Available && Card1.CardInfo.StoreCost.Length > 1;
		Card2Cost1Image.Visible = IsCard2Available && Card2.CardInfo.StoreCost.Length > 0;
		Card2Cost2Image.Visible = IsCard2Available && Card2.CardInfo.StoreCost.Length > 1;
		Card3Cost1Image.Visible = IsCard3Available && Card3.CardInfo.StoreCost.Length > 0;
		Card3Cost2Image.Visible = IsCard3Available && Card3.CardInfo.StoreCost.Length > 1;

		Card1Cost1Label.Visible = IsCard1Available && Card1.CardInfo.StoreCost.Length > 0;
		Card1Cost2Label.Visible = IsCard1Available && Card1.CardInfo.StoreCost.Length > 1;
		Card2Cost1Label.Visible = IsCard2Available && Card2.CardInfo.StoreCost.Length > 0;
		Card2Cost2Label.Visible = IsCard2Available && Card2.CardInfo.StoreCost.Length > 1;
		Card3Cost1Label.Visible = IsCard3Available && Card3.CardInfo.StoreCost.Length > 0;
		Card3Cost2Label.Visible = IsCard3Available && Card3.CardInfo.StoreCost.Length > 1;
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

		Card1Cost1Image.Visible = false;
		Card1Cost2Image.Visible = false;
		Card2Cost1Image.Visible = false;
		Card2Cost2Image.Visible = false;
		Card3Cost1Image.Visible = false;
		Card3Cost2Image.Visible = false;

		Card1Cost1Label.Visible = false;
		Card1Cost2Label.Visible = false;
		Card2Cost1Label.Visible = false;
		Card2Cost2Label.Visible = false;
		Card3Cost1Label.Visible = false;
		Card3Cost2Label.Visible = false;
	}

	public void Refill()
	{
		Card1.SetCard(Cards.GetRandomCard());
		Card2.SetCard(Cards.GetRandomCard());
		Card3.SetCard(Cards.GetRandomCard());

		IsCard1Available = true;
		IsCard2Available = true;
		IsCard3Available = true;

		if (Card1.CardInfo.StoreCost.Length > 0)
		{
			Card1Cost1Image.Texture = GD.Load<Texture2D>(Card1.CardInfo.StoreCost[0].Type.ResourceLocation());
			Card1Cost1Label.Text = $"{Card1.CardInfo.StoreCost[0].Amount}";
		}
		if (Card1.CardInfo.StoreCost.Length > 1)
		{
			Card1Cost2Image.Texture = GD.Load<Texture2D>(Card1.CardInfo.StoreCost[1].Type.ResourceLocation());
			Card1Cost2Label.Text = $"{Card1.CardInfo.StoreCost[1].Amount}";
		}

		if (Card2.CardInfo.StoreCost.Length > 0)
		{
			Card2Cost1Image.Texture = GD.Load<Texture2D>(Card2.CardInfo.StoreCost[0].Type.ResourceLocation());
			Card2Cost1Label.Text = $"{Card2.CardInfo.StoreCost[0].Amount}";
		}
		if (Card2.CardInfo.StoreCost.Length > 1)
		{
			Card2Cost2Image.Texture = GD.Load<Texture2D>(Card2.CardInfo.StoreCost[1].Type.ResourceLocation());
			Card2Cost2Label.Text = $"{Card2.CardInfo.StoreCost[1].Amount}";
		}

		if (Card3.CardInfo.StoreCost.Length > 0)
		{
			Card3Cost1Image.Texture = GD.Load<Texture2D>(Card3.CardInfo.StoreCost[0].Type.ResourceLocation());
			Card3Cost1Label.Text = $"{Card3.CardInfo.StoreCost[0].Amount}";
		}
		if (Card3.CardInfo.StoreCost.Length > 1)
		{
			Card3Cost2Image.Texture = GD.Load<Texture2D>(Card3.CardInfo.StoreCost[1].Type.ResourceLocation());
			Card3Cost2Label.Text = $"{Card3.CardInfo.StoreCost[1].Amount}";
		}
	}
}
