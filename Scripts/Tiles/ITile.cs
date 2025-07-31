
using Godot;
using System.Text.Json.Serialization;

public interface ITile
{
	public TileType Type { get; }

	public virtual void ResetData(Vector2I postion) { }
	public virtual void SimulateSecond(int second, Vector2I thisPostion, TileGrid grid) { }
}


