
using Godot;
using System;

public partial class Death : Node2D
{
	private Sprite2D GraveFace { get; set; }
	private Farmer Farmer { get; set; }
	public Button NextGenerationButton { get; set; }
	public RandomPitchAudioStream DeathSound { get; set; }
	public Action CallBack { get; set; }

	public bool IsOpen { get; set; }

	public override void _Ready()
	{
		GraveFace = GetNode<Sprite2D>("GraveFace");
		Farmer = GetNode<Farmer>("Farmer");
		DeathSound = GetNode<RandomPitchAudioStream>("DeathSound");
		NextGenerationButton = GetNode<Button>("NextGenerationButton");
	}

	public override void _Process(double delta)
	{
		if (IsOpen && NextGenerationButton.ButtonPressed)
		{
			Visible = false;
			IsOpen = false;
			CallBack?.Invoke();
		}
	}

	public void Open(Color oldColor, Color newColor)
	{
		GraveFace.Modulate = oldColor;
		Farmer.SetColor(newColor);

		Visible = true;
		DeathSound.Play();
		IsOpen = true;
	}
}
