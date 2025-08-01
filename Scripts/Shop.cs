using Godot;
using System;

public partial class Shop : Control
{
	public bool IsOpen { get; private set; }

	private Button OpenShopButton { get; set; }
	private Button CloseShopButton { get; set; }
	private ColorRect Background { get; set; }

	private Card Card1 { get; set; }
	private Card Card2 { get; set; }
	private Card Card3 { get; set; }

	public override void _Ready()
	{
		IsOpen = false;

		OpenShopButton = GetNode<Button>("OpenShopButton");
		CloseShopButton = GetNode<Button>("CloseShopButton");
		Background = GetNode<ColorRect>("Background");

		OpenShopButton.Visible = !IsOpen;
		CloseShopButton.Visible = IsOpen;
		Background.Visible = IsOpen;
	}

	public void Open()
	{
		IsOpen = true;
		OpenShopButton.Visible = !IsOpen;
		CloseShopButton.Visible = IsOpen;
		Background.Visible = IsOpen;
	}

	public void Close()
	{
		IsOpen = false;
		OpenShopButton.Visible = !IsOpen;
		CloseShopButton.Visible = IsOpen;
		Background.Visible = IsOpen;
	}
}
