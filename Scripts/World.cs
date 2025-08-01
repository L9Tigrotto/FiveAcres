
using Godot;
using System.Collections.Generic;

public partial class World : Node
{
	[Export] public Vector2I Size { get; set; }

	private List<Generation> Generations { get; set; }
	public Generation CurrentGeneration { get { return Generations[^1]; } }

	private TileMapLayer TileMapLayer { get; set; }
	private Label GenerationLabel { get; set; }
	private Label TimeLeftLabel { get; set; }
	private Shop ShopMenu { get; set; }
	private CardHand CardHand { get; set; }

	public override void _Ready()
	{
		TileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
		TileMapLayer.Transform = new Transform2D(0, new(
			GetViewport().GetVisibleRect().Size.X / 3,
			GetViewport().GetVisibleRect().Size.Y / 2 - Size.Y * TileMapLayer.TileSet.TileSize.Y / 2));

		Storage storage = GetNode<Storage>("Storage");
		Generations = [new(Size, TileMapLayer, storage, 60 * 5)];

		GenerationLabel = GetNode<Label>("GenerationLabel");
		UpdateGenerationLabel();

		TimeLeftLabel = GetNode<Label>("TimeLeftLabel");
		UpdateTimeLeftLabel();

		ShopMenu = GetNode<Shop>("Shop");
		CardHand = GetNode<CardHand>("CardHand");

		CardHand.AddCard(Cards.GetSpecificOrRandomCard("Weed Manicure"));
	}

	private double Elapsed { get; set; } = 0;
	public override void _Process(double delta)
	{
		if (ShopMenu.IsOpen) { return; }

		if (CurrentGeneration.IsEnded)
		{
			Generation nextGeneration = CurrentGeneration.Advance();
			Generations.Add(nextGeneration);
			UpdateGenerationLabel();

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
		if (!@event.IsActionPressed("Click")) { return; }

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

			cardInfo.Use(areaOfEffect, CurrentGeneration.TileGrid);
			CardHand.RemoveSelectedCard();
			CurrentGeneration.ElapsedSeconds += cardInfo.PlayCost;
			UpdateTimeLeftLabel();
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
