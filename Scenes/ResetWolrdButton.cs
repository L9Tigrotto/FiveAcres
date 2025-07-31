using Godot;

public partial class ResetWolrdButton : Button
{
	public override void _Pressed()
	{
		World world = (World)GetParent()!;
		world.Reset();
	}
}
