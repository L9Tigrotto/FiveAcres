using Godot;

public partial class CloseShopButton : Button
{
	private Shop Shop { get; set; }

	public override void _Ready()
	{
		Shop = GetParent<Shop>();
	}

	public override void _Pressed()
	{
		Shop.Close();
	}
}
