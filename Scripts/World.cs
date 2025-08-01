
using Godot;
using System.Collections.Generic;

public partial class World : Node
{
	[Export] public Vector2I Size { get; set; }

	private List<Generation> Generations { get; set; }
	public Generation CurrentGeneration { get { return Generations[^1]; } }

	private Label GenerationLabel { get; set; }
	private Label TimeLeftLabel { get; set; }

	public override void _Ready()
	{
		TileMapLayer tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
		tileMapLayer.Transform = new Transform2D(0, new(
			GetViewport().GetVisibleRect().Size.X / 3,
			GetViewport().GetVisibleRect().Size.Y / 2 - Size.Y * tileMapLayer.TileSet.TileSize.Y / 2));
		GD.Print($"TileMapLayer Transform: {tileMapLayer.Transform}");
		
		Generations = [new(Size, tileMapLayer, 60)];

		GenerationLabel = GetNode<Label>("GenerationLabel");
		UpdateGenerationLabel();

		TimeLeftLabel = GetNode<Label>("TimeLeftLabel");
		UpdateTimeLeftLabel();
	}

	private double Elapsed { get; set; } = 0;
	private bool IsGenerationEnded { get; set; } = false;
	public override void _Process(double delta)
	{
		if (IsGenerationEnded)
		{
			Generation nextGeneration = CurrentGeneration.Advance();
			Generations.Add(nextGeneration);
			IsGenerationEnded = false;

			UpdateGenerationLabel();
			return;
		}

		if (Elapsed >= 1.0)
		{
			IsGenerationEnded = CurrentGeneration.SimulateSecond();
			Elapsed = 0.0;

			UpdateTimeLeftLabel();
		}
		else { Elapsed += delta; }
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
