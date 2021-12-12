using Godot;
using System;
using Game.Extensions;
using static Game.Actor.MoveType;
using static Game.Actor.Direction;


// The actor is a parent class so it shouldn't have any active parts

namespace Game
{
	public class Actor : KinematicBody2D
	{
		public enum Direction
		{
			Left = -1,
			Right = 1,
		}
		public enum MoveType
		{
			Linear,
			Exponential
		}

		public const float gravity = 9.667f * 100;

		public (float Accel, float Decel, float Speed, float JumpHeight) move;

		public Vector2 Velocity = Vector2.Zero;

		/// <summary>
		/// This function is used to implement basic movement.
		/// The movement can be executed as:
		/// <list type="bullet">
		/// <item>
		/// <term><paramref name="moveType" type=".Linear"/></term>
		/// <description>Will set actor's velocity to <b>moveSpeed</b> instantly.</description>
		/// </item>
		/// <item>
		/// <term><paramref name="moveType" type=".Exponential"/></term>
		/// <description>The actor velocity will increase to <b>moveSpeed</b> by <b>moveAccel</b> increments.</description>
		/// </item>
		/// </list>
		/// </summary>
		/// <param name="myDir"></param>
		/// <param name="moveType"></param>
		public void Move(Direction myDir, MoveType moveType = Linear, bool isMoving = true)
		{
			switch (moveType)
			{
				case Linear when isMoving:
					Velocity.x = move.Speed * (int)myDir;
					break;
				case Linear when !isMoving:
					Velocity.x = 0;
					break;

				case Exponential when isMoving:
					Velocity.x = (myDir == Right) ? Math.Min(Velocity.x + move.Accel, move.Speed) : Math.Max(Velocity.x - move.Accel, -move.Speed);
					break;
				case Exponential when !isMoving:
					Velocity.x = (myDir == Right) ? Math.Max(Velocity.x - move.Decel, 0) : Math.Min(Velocity.x + move.Decel, 0);
					break;
			}
		}

		public void Jump(bool condition = true)
		{
			if (condition)
			{
				Velocity.y = move.JumpHeight;
			}

		}

		public void Die()
        {
			QueueFree();
        }


		public void ApplyMovement(float delta)
		{
			Velocity.y += gravity * delta;
			Velocity = MoveAndSlide(Velocity, Vector2.Up);
		}

	}
}
