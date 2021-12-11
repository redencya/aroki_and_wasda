using Godot;
using System;

public static class HoldTime
{
	public static float Hold = 0;
	public static bool isActive = false;
	public static void Calc(float delta)
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
	public static bool isTapped()
	{
		return (Hold <= 0.12f && Hold != 0);
	}
	public static bool isHeld()
	{
		return (Hold > 0.12f);
	}
	public static void Reset()
	{
		if (Hold > 0 && !isActive)
		{
			Hold = 0f;
		}
	}
}