using Godot;
using System;

public partial class RandomPitchAudioStream : AudioStreamPlayer
{
	public new void Play(float fromPosition = 0)
	{
		PitchScale = (float)GD.RandRange(0.8, 1.2);
		base.Play(fromPosition);
	}
}
