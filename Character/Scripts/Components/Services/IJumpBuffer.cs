using System;

namespace UNNAMEDGAME.Game.Character
{
    public interface IJumpBuffer
    {
        public int AirbornJumpCount { get; }
        float CoyoteTimer { get; }
        float JumpBufferTimer { get; }
        void UpdateTimers();
        void HandleJump();
    }
}