using Godot;
using System;

public partial class OpenShopButton : Button
{
	private Shop Shop { get; set; }

	public override void _Ready()
	{
		Shop = GetParent<Shop>();
	}

	public override void _Pressed()
	{
		GD.Print("Shop opened");
		Shop.Open();
	}
}
