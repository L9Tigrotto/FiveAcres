using Godot;
using System;
using System.Diagnostics;

public partial class Shop : Control
{
	public bool IsOpen { get; private set; }

	private Button OpenShopButton { get; set; }
	private Button CloseShopButton { get; set; }
	private TextureRect Background { get; set; }

	private CardShop Card1 { get; set; }
	private CardShop Card2 { get; set; }
	private CardShop Card3 { get; set; }

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

	private Label ComeBackLabel { get; set; }

	private Sprite2D Card1Sold { get; set; }
	private Sprite2D Card2Sold { get; set; }
	private Sprite2D Card3Sold { get; set; }

	private Storage Storage { get; set; }
	private CardHand CardHand { get; set; }

	private RandomPitchAudioStream BuySound { get; set; }
	private RandomPitchAudioStream CantBuySound { get; set; }

	public override void _Ready()
	{
		Storage = GetTree().GetRoot().GetNode<Storage>("World/Storage");
		CardHand = GetTree().GetRoot().GetNode<CardHand>("World/CardHand");
		IsOpen = false;

		OpenShopButton = GetNode<Button>("OpenShopButton");
		CloseShopButton = GetNode<Button>("CloseShopButton");
		Background = GetNode<TextureRect>("Background");

		Card1 = GetNode<CardShop>("Card1");
		Card2 = GetNode<CardShop>("Card2");
		Card3 = GetNode<CardShop>("Card3");

		OpenShopButton.Visible = !IsOpen;
		CloseShopButton.Visible = IsOpen;
		Background.Visible = IsOpen;

		Card1.Visible = false;
		Card2.Visible = false;
		Card3.Visible = false;

		Card1.CardSelected += BuyCard;
		Card2.CardSelected += BuyCard;
		Card3.CardSelected += BuyCard;

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

		Card1Sold = GetNode<Sprite2D>("Card1Sold");
		Card2Sold = GetNode<Sprite2D>("Card2Sold");
		Card3Sold = GetNode<Sprite2D>("Card3Sold");

		Card1Sold.Visible = false;
		Card2Sold.Visible = false;
		Card3Sold.Visible = false;

		ComeBackLabel = GetNode<Label>("ComeBackLater");

		Refill();

		BuySound = GetNode<RandomPitchAudioStream>("BuySound");
		CantBuySound = GetNode<RandomPitchAudioStream>("CantBuySound");
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

		Card1Sold.Visible = !IsCard1Available;
		Card2Sold.Visible = !IsCard2Available;
		Card3Sold.Visible = !IsCard3Available;

		if (!IsCard1Available && !IsCard2Available && !IsCard3Available)
			ComeBackLabel.Visible = true;
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

		Card1Sold.Visible = false;
		Card2Sold.Visible = false;
		Card3Sold.Visible = false;

		ComeBackLabel.Visible = false;
	}

	public void Refill()
	{
		if (!IsCard1Available) Card1.SetCard(Cards.GetRandomCard());
		if (!IsCard2Available) Card2.SetCard(Cards.GetRandomCard());
		if (!IsCard3Available) Card3.SetCard(Cards.GetRandomCard());

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

	void BuyCard(int cardIndex)
	{
		if (cardIndex > 2) return;

		CardShop cardToBuy = cardIndex switch
		{
			0 => Card1,
			1 => Card2,
			2 => Card3,
			_ => throw new UnreachableException()
		};

		//Can afford?
		for (int i = 0; i < cardToBuy.CardInfo.StoreCost.Length; i++)
		{
			ItemType itemType = cardToBuy.CardInfo.StoreCost[i].Type;
			int costAmount = cardToBuy.CardInfo.StoreCost[i].Amount;

			if (Storage.Count[itemType] < costAmount)
			{
				CantBuySound.Play();
				return;
			}
		}

		//If can afford buy and hide card

		BuySound.Play();

		for (int i = 0; i < cardToBuy.CardInfo.StoreCost.Length; i++) //Sta cosa e' un po' demente ma vbb
		{
			ItemType itemType = cardToBuy.CardInfo.StoreCost[i].Type;
			int costAmount = cardToBuy.CardInfo.StoreCost[i].Amount;

			Storage.AddItem(itemType, -costAmount);
		}

		SetCardSlotVisibility(cardIndex, false);
		CardHand.AddCard(cardToBuy.CardInfo);
		//Close();
	}

	void SetCardSlotVisibility(int cardIndex, bool isVisible)
	{
		switch (cardIndex)
		{
			case 0:
				Card1.Visible = isVisible;
				Card1Cost1Label.Visible = isVisible;
				Card1Cost1Image.Visible = isVisible;

				if (Card1.CardInfo.StoreCost.Length > 1)
				{
					Card1Cost2Label.Visible = isVisible;
					Card1Cost2Image.Visible = isVisible;
				}

				Card1Sold.Visible = !isVisible;
				IsCard1Available = isVisible;
				break;

			case 1:
				Card2.Visible = isVisible;
				Card2Cost1Label.Visible = isVisible;
				Card2Cost1Image.Visible = isVisible;

				if (Card2.CardInfo.StoreCost.Length > 1)
				{
					Card2Cost2Label.Visible = isVisible;
					Card2Cost2Image.Visible = isVisible;
				}

				Card2Sold.Visible = !isVisible;
				IsCard2Available = isVisible;
				break;

			case 2:
				Card3.Visible = isVisible;
				Card3Cost1Label.Visible = isVisible;
				Card3Cost1Image.Visible = isVisible;

				if (Card3.CardInfo.StoreCost.Length > 1)
				{
					Card3Cost2Label.Visible = isVisible;
					Card3Cost2Image.Visible = isVisible;
				}

				Card3Sold.Visible = !isVisible;
				IsCard3Available = isVisible;
				break;
		}
	}
}
