using Godot;
using System;
using Godot.Collections;

public partial class CardHand : Node2D
{
	[Export] public PackedScene CardScene { get; set; }
	[Export] public int CardSize { get; set; }
	[Export] public int CardSpacing { get; set; }

	[Export] public int CardCurvatureMaxHeight { get; set; }
	[Export] public Curve CardCurvature { get; set; }

	Array<Card> CardsInHand = [];
	public Card SelectedCard { get; set; }
	public int CardCount { get { return CardsInHand.Count; } }

	int cardAmount = 0;

	private AudioStreamPlayer PickSound { get; set; }

	public override void _Ready()
	{
		PickSound = GetNode<AudioStreamPlayer>("PickSound");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("ui_accept")) AddCard(Cards.GetRandomCard());
	}

	public void AddCard(ICard card)
	{
		Card newCard = CardScene.Instantiate<Card>();
		newCard.Position = Vector2.Down * CardSize;
		newCard._Ready();
		newCard.SetCard(card);

		//Add card to cards in hand
		CardsInHand.Add(newCard);
		AddChild(newCard);

		newCard.CardSelected += OnCardClicked;

		RefreshPositions();
	}

	public void RemoveSelectedCard()
	{
		CardsInHand.Remove(SelectedCard);
		SelectedCard.QueueFree();
		SelectedCard = null;
		RefreshPositions();
	}

	void OnCardClicked(Card card)
	{
		PickSound.Play();
		if (SelectedCard == card)
		{
			SelectedCard = null;
			return;
		}

		SelectedCard?.ToggleSelection();
		SelectedCard = card;
	}

	public void RefreshPositions()
	{
		Vector2 spacing = new(CardSize + CardSpacing, 0);

		for (int i = 0; i < CardsInHand.Count; i++)
		{
			Card card = CardsInHand[i];
			Tween tween = card.GetTree().CreateTween();

			//Calculate for each card in hand their new position
			Vector2 newPosition = i * spacing;

			if (CardsInHand.Count > 1)
				newPosition += Vector2.Down * (CardSize / 2.0f - CardCurvature.Sample((float)i / (CardsInHand.Count - 1)) * CardCurvatureMaxHeight);

			//Tween from oldPos to newPos
			tween.TweenProperty(card, "position", newPosition, 0.08f).SetTrans(Tween.TransitionType.Elastic);
		}
	}
}
