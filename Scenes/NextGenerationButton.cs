using Godot;
using System;

public partial class NextGenerationButton : Button
{
	public override void _Pressed()
	{
		World world = (World)GetParent()!;
		//world.TileGrid.NextGeneration();
	}
}
