using Godot;
using System;
using Game.Extensions;
using static Game.Actor.MoveType;
using static Game.Actor.Direction;
using static Game.PlayerController.MoveMode;

namespace Game
{
	public class PlayerController : Actor
	{
		public enum MoveMode
		{
			Regular,
			Swimming,
		}

		public Direction currentDirection = Right;
		public MoveMode currentMoveMode = Regular;

		// Cache of constants for movement
		private ((float Air, float Ground) Accel, (float Air, float Ground) Decel) value = ((12f, 17f) , (27f, 33f));

		public void DefineMovement(MoveMode myMode = Regular)
		{
			switch (myMode)
			{
				case Regular:
					TemplateMovement();
					break;
			}
		}
		// build a template for movement modes with this - now boiled down to Flip, Move and Jump for convenience!
		public void TemplateMovement()
		{
			// value assignment for Actor variables related to movement
			move.Accel = (IsOnFloor()) ? value.Accel.Ground : value.Accel.Air ;
			move.Decel = (IsOnFloor()) ? value.Decel.Ground : value.Decel.Air ;
			move.Speed = 2.95f * 100;
			move.JumpHeight = -5.5f * 100;

			// IF you tap, you flip the character
			// Unless you're on a sticky surface, then you do that AND rebound from the surface
			if (HoldTime.isTapped()) { currentDirection = currentDirection.Flip(); }

			// MOVE
			// IF you actively hold down the action key
			if (HoldTime.isHeld(true)) { Move(currentDirection, Exponential); }

			// JUMP
			// IF you held the key but you're no longer holding it
			else if (HoldTime.isHeld(false)) { Jump(IsOnFloor()); }
			
			if (!HoldTime.isActive && Velocity.x != 0) { Move(currentDirection, Exponential, false); }
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
			HoldTime.IncrementBy(delta);
			GD.Print($"{Velocity.x} {currentDirection}");
		}

		internal class MovementTemplate
        {
			// This should be an easy-to-repeat, scaleable solution for implementing new gamemodes using the Tap | Hold | Release inputs.
			// 
        }
	}
}
