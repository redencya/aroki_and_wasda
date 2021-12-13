using Godot;
using System;
using static Game.Actor;
using Game.Extensions;

public class DebugDisplay : Control
{
    public override void _Process(float delta)
    {

    }

    public void UpdateDisplay(Vector2 velo, Direction myDir, float hold)
    {
        Label DisplayDirection = GetNode<Label>("VBoxContainer/Label");
        Label DisplayTechnical = GetNode<Label>("VBoxContainer/Label2");

        Position2D PlayerSpawner = GetNode<Position2D>("../PlayerSpawner");
        KinematicBody2D Player = PlayerSpawner.GetNode<KinematicBody2D>("/Player");

        DisplayDirection.Text = $"{velo} | {myDir}";
        DisplayTechnical.Text = $"{hold} | state";
    }
}
        