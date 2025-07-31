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

	Array<Card> CardsInHand = new();
	Card SelectedCard { get; set; }
    
	int cardAmount = 0;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("ui_accept")) AddCard();
	}
    
	public void AddCard()
	{
		Card newCard = CardScene.Instantiate<Card>();
		newCard.Position = Vector2.Down * CardSize;

		//Add card to cards in hand
		CardsInHand.Add(newCard);
		AddChild(newCard);

		newCard.CardSelected += OnCardClicked;
        
		RefreshPositions();
	}
    
	void OnCardClicked(Card card)
	{
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
		float totalSpace = (CardSize + CardSpacing) * (CardsInHand.Count - 1);
		Vector2 startPosition = new(-totalSpace / 2, 0);
		Vector2 spacing = new(CardSize + CardSpacing, 0);

        
		for (int i = 0; i < CardsInHand.Count; i++)
		{
			Card card = CardsInHand[i];
			Tween tween = card.GetTree().CreateTween();

			//Calculate for each card in hand their new position
			Vector2 newPosition = startPosition + i * spacing;
			newPosition += Vector2.Down * (CardSize/2.0f - CardCurvature.Sample((float)i/(CardsInHand.Count-1)) * CardCurvatureMaxHeight);

			//Tween from oldPos to newPos
			tween.TweenProperty(card, "position", newPosition, 0.08f).SetTrans(Tween.TransitionType.Elastic);
		}
	}
}
