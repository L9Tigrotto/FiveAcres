
using Godot;
using System.Collections.Generic;

public partial class World : Node
{
	[Export] public Vector2I Size { get; set; }
	private List<Generation> Generations { get; set; }

	public Generation CurrentGeneration { get { return Generations[^1]; } }

	public override void _Ready()
	{
		TileMapLayer tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
		Generations = [new(Size, tileMapLayer, 3 * 60)];
	}

	private double elapsed { get; set; }
	public override void _Process(double delta)
	{
		if (elapsed >= 1.0) { CurrentGeneration.SimulateSecond(); elapsed = 0.0; }
		else { elapsed += delta; }
	}

	public void AdvanceGeneration()
	{
		Generation nextGeneration = CurrentGeneration.Advance();
		Generations.Add(nextGeneration);
	}

	public void Reset()
	{
		TileMapLayer tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
		Generations.Clear();
		Generations.Add(new(Size, tileMapLayer, 3 * 60));
	}
}
