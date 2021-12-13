using Godot;
using System;
using static PlayerSpawner;

public class Hazard : Area2D
{
    [Signal]
    delegate void KillPlayer();

    [Export]
    public NodePath playerSpawnerPath;
    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    public override void _Ready()
    {
        base._Ready();
        Connect("KillPlayer", GetNode(playerSpawnerPath), "KilledByHazards");
    }

    public void _on_Hazards_body_entered(Node body)
    {
        if (body.Name.Contains("Player"))
        {
            EmitSignal("KillPlayer");
        }
    }
}
