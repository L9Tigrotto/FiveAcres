using Godot;

public partial class Farmer : Node2D
{
	private AnimatedSprite2D AnimatedSprite { get; set; }
	private Sprite2D Face { get; set; }
	public int CurrentStage { get; private set; }
	public Color Color { get; private set; }

	public override void _Ready()
	{
		AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
		Face = GetNode<Sprite2D>("Face");

		Color = new Color(
			(float)(GD.Randf() * 0.8 + 0.1),
			(float)(GD.Randf() * 0.8 + 0.1),
			(float)(GD.Randf() * 0.8 + 0.1),
			1);

		Face.Modulate = Color;
	}

	public void Age(int stage)
	{
		if (CurrentStage >= stage) { return; }
		CurrentStage = stage;
		AnimatedSprite.Frame = CurrentStage;
	}

	public void RerollColor()
	{
		Color = new Color(
			(float)(GD.Randf() * 0.8 + 0.1),
			(float)(GD.Randf() * 0.8 + 0.1),
			(float)(GD.Randf() * 0.8 + 0.1),
			1);
		Face.Modulate = Color;

		CurrentStage = 0;
		AnimatedSprite.Frame = CurrentStage;
	}

	public void SetColor(Color color)
	{
		Color = color;
		Face.Modulate = Color;
	}
}
