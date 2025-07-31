using Godot;
using System;

public partial class Card : Node2D
{
	[Export] public CardData Data { get; set; }
	
	Sprite2D CardArt { get; set; }
	Label CardName { get; set; }
	Label CardDescription { get; set; }
	
	public override void _Ready()
	{
		if(Data is null) throw new NullReferenceException($"No card data was assigned to card.");
		
		CardArt = GetNode<Sprite2D>("Card_Bg/Card_Art");
		CardName = GetNode<Label>("Card_Name");
		CardDescription = GetNode<Label>("Card_Desc");
		
		CardArt.Texture = Data.Art;
		CardName.Text = Data.Name;
		CardDescription.Text = Data.Description;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
