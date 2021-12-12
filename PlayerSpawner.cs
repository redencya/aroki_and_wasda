using Godot;
using System;

public class PlayerSpawner : Position2D
{
    private PackedScene PlayerRef = (PackedScene)ResourceLoader.Load("res://Player.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        RecreatePlayer();
        // CONNECT signal "SpawnNewPlayer" with no extra params
    }

    public KinematicBody2D InstancePlayer()
    {
        KinematicBody2D newPlayer = (KinematicBody2D)PlayerRef.Instance();
        return newPlayer;
    }

    public void CreatePlayer()
    {
        AddChild(InstancePlayer());
    }

    public void RecreatePlayer()
    {
        InstancePlayer().QueueFree();
        CreatePlayer();
    }



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
