
using Godot;

public interface ITile
{
	public TileType Type { get; }

	public int MinHarvest { get; }
	public int MaxHarvest { get; }

	public virtual void ResetData(Vector2I postion) { }
	public virtual void SimulateSecond(int second, Vector2I thisPostion, TileGrid grid) { }
	public virtual bool TryClick(Vector2I thisPostion, TileGrid grid, Storage storage) { return false; }
}


