using Godot;
using System;

public partial class Card : Area2D
{
	//[Export] public CardBaseData Data { get; set; }
	
	Sprite2D CardBg { get; set; }
	Sprite2D CardArt { get; set; }
	Label CardName { get; set; }
	Label CardDescription { get; set; }

	Vector2 _originalPosition;
	Vector2 _originalScale;	
	bool _isSelected;
	
	[Signal]
	public delegate void CardSelectedEventHandler(Card card);

	
	public override void _Ready()
	{
		CardBg = GetNode<Sprite2D>("Card_Bg");
		CardArt = CardBg.GetNode<Sprite2D>("Card_Art");
		CardName = CardBg.GetNode<Label>("Card_Name");
		CardDescription = CardBg.GetNode<Label>("Card_Desc");
		
		_originalPosition = CardBg.Position;
		_originalScale = CardBg.Scale;
		
		//CardArt.Texture = Data.Art;
		//CardName.Text = Data.Name;
		//CardDescription.Text = Data.Description;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton { Pressed: true, ButtonIndex: MouseButton.Left })
			ToggleSelection();
	}
	
	public void ToggleSelection()
	{
		if (_isSelected)
		{
			CardBg.ZIndex = 0;
			CardBg.Position = _originalPosition;
			CardBg.Scale = _originalScale;
			_isSelected = false;
		}
		else
		{
			_originalPosition = CardBg.Position;
			
			CardBg.ZIndex = 10;
			CardBg.Position = new Vector2(0, -(CardBg.Texture.GetHeight()));
			CardBg.Scale = _originalScale * new Vector2(1.25f, 1.25f);
			_isSelected = true;
		}
			
		EmitSignal(SignalName.CardSelected, this);
	}
}
