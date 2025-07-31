
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
		Generations = [new(id: 0, Size, tileMapLayer)];
	}

	public void Advance()
	{
		Generation nextGeneration = CurrentGeneration.Advance();
		Generations.Add(nextGeneration);
	}

	public void Reset()
	{
		TileMapLayer tileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
		Generations.Clear();
		Generations.Add(new(id: 0, Size, tileMapLayer));
	}
}
