using Godot;

[GlobalClass]
public partial class CardData : Resource
{
    [Export] public Texture2D Art { get; set; }
    [Export] public string Name { get; set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; set; }
}
