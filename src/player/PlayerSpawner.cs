using Godot;
using System;

public class PlayerSpawner : Position2D
{
	private PackedScene PlayerRef = (PackedScene)ResourceLoader.Load("res://src/player/Player.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreatePlayer();
		// CONNECT signal "SpawnNewPlayer" with no extra params
	}

	public KinematicBody2D InstancePlayer()
	{
		KinematicBody2D newPlayer = (KinematicBody2D)PlayerRef.Instance();
		newPlayer.Name = "Player";
		return newPlayer;
	}

	public void CreatePlayer()
	{
		AddChild(InstancePlayer());
	}

	public void RecreatePlayer()
	{
		GetChild(0).QueueFree();
		CreatePlayer();
	}

	public void KilledByHazards()
	{
		RecreatePlayer();
	}




//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
