
using Godot;
using System.Diagnostics;

public partial class World : Node
{
	[Export] public Vector2I Size { get; set; }
	TileMapLayer TileMapLayer { get; set; }
	public ITile[,] Grid { get; set; }
	public FastNoiseLite RiverNoise { get; set; }
	public Vector2 RiverFlow { get; set; }
	public int Generation { get; set; }

	public override void _Ready()
	{
		TileMapLayer = GetNode<TileMapLayer>("TileMapLayer");
		Grid = new ITile[Size.X, Size.Y];

		ResetWorld();
	}

	public void ResetWorld()
	{
		RiverNoise = new((int)Stopwatch.GetTimestamp());
		RiverNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
		RiverNoise.SetFrequency(0.030f);
		RiverNoise.SetFractalType(FastNoiseLite.FractalType.Ridged);
		RiverNoise.SetFractalOctaves(1);

		RiverFlow = new Vector2(RiverNoise.GetNoise(0, 0), RiverNoise.GetNoise(1, 1)).Normalized() / 10;
		Generation = 0;

		FirstGeneration();
	}

	private void FirstGeneration()
	{
		for (int x = 0; x < Size.X; x++)
		{
			for (int y = 0; y < Size.Y; y++)
			{
				// z is used to simulate erosion
				float value = RiverNoise.GetNoise(x + RiverFlow.X * Generation, y + RiverFlow.Y * Generation, Generation / 10.0f);

				// interpolate random value from [-1, 1] to [0, 1]
				value = (value + 1) / 2;

				if (value > 0.93) { SetCell(x, y, Tiles.Of(TileType.Water)); }
				else { SetCell(x, y, Tiles.Of(TileType.Grass)); }
			}
		}

		Generation++;
	}

	public void NextGeneration()
	{
		for (int x = 0; x < Size.X; x++)
		{
			for (int y = 0; y < Size.Y; y++)
			{
				// z is used to simulate erosion
				float value = RiverNoise.GetNoise(x + RiverFlow.X * Generation, y + RiverFlow.Y * Generation, Generation / 10.0f);

				// interpolate random value from [-1, 1] to [0, 1]
				value = (value + 1) / 2;

				if (value > 0.93) { SetCell(x, y, Tiles.Of(TileType.Water)); }
				else { SetCell(x, y, Tiles.Of(TileType.Grass)); }
			}
		}

		Generation++;
	}

	private void SetCell(int x, int y, ITile tile)
	{
		TileMapLayer.SetCell(new(x, y), 0, tile.Type.ToAtlasCoord());
		Grid[x, y] = tile;
	}
}
