using Godot;
using System;
using Game.Extensions;
using static Game.Actor.MoveType;
using static Game.Actor.Direction;
using static Game.Player.MoveMode;

namespace Game
{
	public class Player : Actor
	{
		public enum MoveMode
		{
			Regular,
			Swimming,
		}

		public Direction currentDirection = Right;
		public MoveMode currentMoveMode = Regular;

		// Cache of constants for movement
		private const float airAccel = 12f;
		private const float groundAccel = 17f;
		private const float airDecel = 27f;
		private const float groundDecel = 33f;

		public void DefineMovement(MoveMode myMode = Regular)
		{
			// value assignment for Actor variables related to movement
			moveAccel = (IsOnFloor()) ? groundAccel : airAccel;
			moveDecel = (IsOnFloor()) ? groundDecel : airDecel;
			moveSpeed = 2.95f * 100;
			moveJumpHeight = -5.5f * 100;

			switch (myMode)
			{
				case Regular:
					TemplateMovement();
					break;
			}
		}
		// build a template for movement modes with this
		public void TemplateMovement()
		{
			if (HoldTime.isTapped() && !HoldTime.isActive)
			{
				currentDirection.Flip();
			}

			if (HoldTime.isHeld() && HoldTime.isActive)
			{
				Move(currentDirection, Exponential);
			}
			else if (HoldTime.isHeld())
			{
				Jump(IsOnFloor());
			}

			if (!HoldTime.isActive && Velocity.x != 0)
			{
				MoveStop(currentDirection, Exponential);
			}
		}

		public override void _PhysicsProcess(float delta)
		{
			base._PhysicsProcess(delta);
			DefineMovement(currentMoveMode);
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
}
