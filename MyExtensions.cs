using Godot;
using System;
using static Game.Actor;
using static Game.Player;
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

    public static class Extensions
    {

    }

}
