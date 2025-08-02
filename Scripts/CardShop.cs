using Godot;
using System;

public partial class CardShop : Area2D
{
	//[Export] public CardBaseData Data { get; set; }

	public ICard CardInfo { get; set; }
	[Export] public int CardIndex { get; set; }

	Sprite2D CardBg { get; set; }
	Sprite2D CardArt { get; set; }
	Label CardCost { get; set; }
	Label CardName { get; set; }
	Label CardDescription { get; set; }
	
	
	[Signal]
	public delegate void CardSelectedEventHandler(int cardIndex);
	Vector2 _originalPosition;
	Vector2 _originalScale;

	public override void _Ready()
	{
		CardBg = GetNode<Sprite2D>("Card_Bg");
		CardArt = GetNode<Sprite2D>("Card_Bg/Card_Art");
		CardCost = GetNode<Label>("Card_Bg/Card_Cost");
		CardName = GetNode<Label>("Card_Bg/Card_Name");
		CardDescription = GetNode<Label>("Card_Bg/Card_Desc");
		
		_originalPosition = Position;
		_originalScale = Scale;
		
		GD.Print(_originalScale);
		//CardArt.Texture = Data.Art;
		//CardName.Text = Data.Name;
		//CardDescription.Text = Data.Description;
	}

	public void SetCard(ICard card)
	{
		CardInfo = card;
		
		CardArt.Texture = card.Image;
		CardCost.Text = $"{card.PlayCost}";
		CardName.Text = card.Name;
		CardDescription.Text = card.Description;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _MouseShapeEnter(int shapeIdx)
	{
		GD.Print("MouseEnter");
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "scale", _originalScale * Vector2.One * 1.25f, 0.2f).SetTrans(Tween.TransitionType.Bounce);
	}
	
	public override void _MouseShapeExit(int shapeIdx)
	{
		GD.Print("MouseExit");
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "scale", _originalScale, 0.2f).SetTrans(Tween.TransitionType.Bounce);
	}

	public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton { Pressed: true, ButtonIndex: MouseButton.Left })
			EmitSignal(SignalName.CardSelected, CardIndex);
	}
}
