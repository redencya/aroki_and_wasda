using Godot;
using System;
using static Actor.MoveType;
using static Actor.Direction;
using static Player.MoveMode;

public class Player : Actor
{
	public enum MoveMode
	{
		Regular,
		Swimming,
	}

	private Direction currentDirection = Right;
	private MoveMode currentMoveMode = Regular;

	// Cache of constants for 
	private const float airAccel = 10f;
	private const float groundAccel = 15f;
	private const float airDecel = 25f;
	private const float groundDecel = 35f;

	public void DefineMovement(MoveMode myMode = Regular)
	{
		moveAccel = (IsOnFloor()) ? groundAccel : airAccel;
		moveDecel = (IsOnFloor()) ? groundDecel : airDecel;
		moveSpeed = 2.5f * 100;
		moveJumpHeight = -5.5f * 100;

		if (HoldTime.isTapped() && !HoldTime.isActive)
		{
			currentDirection = InvertDirection(currentDirection);
		}
		if (HoldTime.isHeld())
		{
			if (HoldTime.isActive)
			{
				Move(currentDirection, Exponential);
			}
			else
			{
				Jump();
			}
		}
		if (!HoldTime.isActive && Velocity.x != 0)
		{
			MoveStop(currentDirection, Exponential);
		}
	}
	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		DefineMovement();
		ApplyMovement(delta);
		HoldTime.Reset();
	}
	public override void _Process(float delta)
	{
		base._Process(delta);
		HoldTime.Calc(delta);
		GD.Print($"{Velocity.x} {currentDirection}");
	}
}