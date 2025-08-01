
using Godot;
using System.Text.Json.Serialization;

public interface ITile
{
	public TileType Type { get; }

	public virtual void ResetData(Vector2I postion) { }
	public virtual void SimulateSecond(int second, Vector2I thisPostion, TileGrid grid) { }
	public virtual void Click(Vector2I thisPostion, TileGrid grid, Storage storage) { }
}


