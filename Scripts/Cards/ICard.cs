
using Godot;
using System.Collections.Generic;

public interface ICard
{
	public Texture2D Image { get; }
	public string Name { get; }
	public string Description { get; }

	public (ItemType Type, int Amount)[] StoreCost { get; }
	public int PlayCost { get; }

	public int Radius { get; }

	public virtual void Use(List<Vector2I> areaOfEffect, TileGrid tileGrid) { }
}

