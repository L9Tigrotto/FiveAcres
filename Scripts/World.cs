
using Godot;
using System.Collections.Generic;

public partial class World : Node2D
{
	[Export] public Vector2I Size { get; set; }

	private List<Generation> Generations { get; set; }
	public Generation CurrentGeneration { get { return Generations[^1]; } }

	private TileMapLayer TileMapLayer { get; set; }
	private Label GenerationLabel { get; set; }
	private Label TimeLeftLabel { get; set; }
	private Shop ShopMenu { get; set; }
	private CardHand CardHand { get; set; }
	private NinePatchRect Cursor { get; set; }

	public override void _Ready()
	{
		TileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
		TileMapLayer.Transform = new Transform2D(0, new(
			GetViewport().GetVisibleRect().Size.X / 3, TileMapLayer.TileSet.TileSize.Y));

		Storage storage = GetNode<Storage>("Storage");
		Generations = [new(Size, TileMapLayer, storage, 60 * 5)];

		GenerationLabel = GetNode<Label>("GenerationLabel");
		UpdateGenerationLabel();

		TimeLeftLabel = GetNode<Label>("TimeLeftLabel");
		UpdateTimeLeftLabel();

		ShopMenu = GetNode<Shop>("Shop");
		CardHand = GetNode<CardHand>("CardHand");

		CardHand.AddCard(Cards.GetSpecificOrRandomCard("Weed Manicure"));
		CardHand.AddCard(Cards.GetSpecificOrRandomCard("Weed Reaper"));
		CardHand.AddCard(Cards.GetSpecificOrRandomCard("Weed Nuker"));

		Cursor = GetNode<NinePatchRect>("Cursor");
	}

	private double Elapsed { get; set; } = 0;
	public override void _Process(double delta)
	{
		if (ShopMenu.IsOpen) { return; }

		if (CurrentGeneration.IsEnded)
		{
			Generations.Add(CurrentGeneration.Advance());
			UpdateGenerationLabel();

			ShopMenu.Refill();
			// TODO: deal cards
			return;
		}

		if (Elapsed >= 1.0)
		{
			CurrentGeneration.SimulateSecond();
			Elapsed = 0.0;

			UpdateTimeLeftLabel();
		}
		else { Elapsed += delta; }
	}

	public override void _Input(InputEvent @event)
	{
		switch (@event)
		{
			case InputEventMouseMotion _: HandleMouseMovement(); break;
			case InputEventMouseButton mouseButtonEvent: HandleMouseClick(mouseButtonEvent); break;
		}
	}

	private void HandleMouseMovement()
	{
		Vector2 position = GetViewport().GetMousePosition();        // mouse position in viewport coordinates
		
		Vector2 tilePosition = position - TileMapLayer.Position;                // mouse position relative to the TileMapLayer
		tilePosition = tilePosition/ TileMapLayer.TileSet.TileSize;        // convert to tile coordinates
		
		bool isInTileSpace = tilePosition.X >= 0 && tilePosition.X < Size.X &&
		                     tilePosition.Y >= 0 && tilePosition.Y < Size.Y;

		if (!isInTileSpace)
		{
			Cursor.Visible = false;
			return;
		}
		
		Vector2I tileCoordinate = new(Mathf.FloorToInt(tilePosition.X), Mathf.FloorToInt(tilePosition.Y));
		Vector2 cursorPosition = TileMapLayer.Position + tileCoordinate * TileMapLayer.TileSet.TileSize;
		
		Cursor.Visible = true;
		Cursor.Position = cursorPosition;

		if (CardHand.SelectedCard is not null)
		{
			int size = (CardHand.SelectedCard.CardInfo.Radius-1) * 2 + 1;

			Cursor.Size = Vector2.One * size * TileMapLayer.TileSet.TileSize;
			Cursor.Position = Cursor.Position - (Cursor.Size/2.0f).Ceil() + TileMapLayer.TileSet.TileSize/2;
			Cursor.Modulate = Colors.Black;
		}
		else
		{
			Cursor.Size = Vector2.One* TileMapLayer.TileSet.TileSize;
			Cursor.Modulate = Colors.White;
		}
	}

	private void HandleMouseClick(InputEventMouseButton mouseButtonEvent)
	{
		if(!mouseButtonEvent.IsActionPressed("Click")) { return; }
		
		Vector2 position = GetViewport().GetMousePosition();        // mouse position in viewport coordinates
		position = position - TileMapLayer.Position;                // mouse position relative to the TileMapLayer
		position = position / TileMapLayer.TileSet.TileSize;        // convert to tile coordinates

		bool isInTileSpace = position.X >= 0 && position.X < Size.X &&
		                     position.Y >= 0 && position.Y < Size.Y;

		if (!isInTileSpace) { return; } // managed by other scripts

		// convert to integer tile coordinates
		Vector2I tileCoordinate = new(Mathf.FloorToInt(position.X), Mathf.FloorToInt(position.Y));

		if (CardHand.SelectedCard is null)
		{
			TileGrid tileGrid = CurrentGeneration.TileGrid;
			tileGrid.Grid[tileCoordinate.X, tileCoordinate.Y].Click(tileCoordinate, tileGrid, CurrentGeneration.Storage);
		}
		else
		{
			Card card = CardHand.SelectedCard;
			ICard cardInfo = card.CardInfo;

			List<Vector2I> areaOfEffect = [];
			for (int x = -cardInfo.Radius; x <= cardInfo.Radius; x++)
			{
				for (int y = -cardInfo.Radius; y <= cardInfo.Radius; y++)
				{
					Vector2I offset = new(x, y);
					Vector2I targetTile = tileCoordinate + offset;

					// check for point validity
					if (targetTile.X < 0 || targetTile.X >= Size.X ||
					    targetTile.Y < 0 || targetTile.Y >= Size.Y) { continue; }

					if (targetTile.DistanceTo(tileCoordinate) < cardInfo.Radius)
					{
						areaOfEffect.Add(targetTile);
					}
				}
			}

			bool isConsumed = cardInfo.Use(areaOfEffect, CurrentGeneration.TileGrid);
			if (isConsumed)
			{
				CardHand.RemoveSelectedCard();
				CurrentGeneration.ElapsedSeconds += cardInfo.PlayCost;
				UpdateTimeLeftLabel();
			}
		}
	}

	private void UpdateGenerationLabel()
	{
		GenerationLabel.Text = $"Generation: {CurrentGeneration.ID}";
	}

	private void UpdateTimeLeftLabel()
	{
		TimeLeftLabel.Text = $"↳ {CurrentGeneration.LengthInSeconds - CurrentGeneration.ElapsedSeconds} seconds";
	}
}
