using Godot;
using System;
using static Game.Actor;
using static Game.PlayerController;
using static Game.Actor.Direction;

namespace Game.Extensions
{
    public static class TypeExtensions
    {
        public static Direction Flip(this Direction myDir)
        {
			return (myDir == Right) ? Left : Right;
        }
    }

	public static class HoldTime
	{
		public static float Hold = 0;
		public static bool isActive = false;
		public static bool isTapped()
		{
			return ((HoldTime.Hold <= 0.12f && HoldTime.Hold != 0) && !HoldTime.isActive);
		}
		public static bool isHeld(bool Is)
		{
			if (Is)
            {
				return ((HoldTime.Hold > 0.12f) && HoldTime.isActive);
			}
			else
            {
				return (HoldTime.Hold > 0.12f);
			}
		}

		public static void IncrementBy(float delta)
		{
			if (Input.IsActionPressed("ui_accept"))
			{
				Hold += delta;
				isActive = true;
			}
			else
			{
				isActive = false;
			}
		}
		public static void Reset()
		{
			if (Hold > 0 && !isActive)
			{
				Hold = 0f;
			}
		}
	}

}
