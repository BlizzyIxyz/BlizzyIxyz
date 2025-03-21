using System;

namespace UNNAMEDGAME.Game.Character
{
    public interface IGroundedChecker
    {
        bool IsGrounded { get; }
        event Action<bool> GroundedStateChanged;
    }
}