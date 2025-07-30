using Godot;
using System;

public partial class ResetWolrdButton : Button
{
	public override void _Pressed()
	{
		World world = (World)GetParent()!;
		world.ResetWorld();
	}
}
