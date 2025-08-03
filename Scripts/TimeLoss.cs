
using Godot;

public partial class TimeLoss : Node2D
{
	private Label Label { get; set; }

	public override void _Ready()
	{
		Label = GetNode<Label>("Label");
	}

	public void Update(int amount, Vector2 position)
	{
		Label.Text = $"-{amount}";
		Position = position;
		Start();
	}

	public void Start()
	{
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", Position + Vector2.Up * 10, 0.6).SetTrans(Tween.TransitionType.Quad);
		tween.TweenProperty(this, "modulate", new Color(Colors.White, 0), 0.6).SetTrans(Tween.TransitionType.Quad);
		tween.Finished += () => QueueFree();
	}
}
