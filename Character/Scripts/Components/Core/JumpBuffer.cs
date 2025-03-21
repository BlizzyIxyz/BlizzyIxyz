using System;
using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public class JumpBuffer : IJumpBuffer, IDisposable
    {
        public int AirbornJumpCount { get; private set; }
        public float CoyoteTimer { get; private set; }
        public float JumpBufferTimer { get; private set; }

        private readonly MovementConfig _movementConfig;
        private readonly IGroundedChecker _groundedChecker;
        private readonly IMovementInputProvider _movementInputProvider;

        public JumpBuffer(MovementConfig movementConfig, IGroundedChecker groundedComponent, IMovementInputProvider movementInputProvider)
        {
            _movementConfig = movementConfig;
            _movementInputProvider = movementInputProvider;
            _groundedChecker = groundedComponent;

            _movementInputProvider.OnJumpRequested += ResetCoyoteTimer;
            _groundedChecker.GroundedStateChanged += ctx => HandleGroundedStateChanged(ctx);
        }

        public void UpdateTimers()
        {
            if (CoyoteTimer > 0)
                CoyoteTimer -= Time.deltaTime;
            if (JumpBufferTimer > 0 && !_groundedChecker.IsGrounded)
                JumpBufferTimer -= Time.deltaTime;
        }
        public void HandleJump()
        {
            if (JumpBufferTimer <= 0f)
                AirbornJumpCount++;

            JumpBufferTimer = 0f;
            CoyoteTimer = 0f;

            Debug.LogFormat($"<color=yellow> [JumpBuffer] </color>" +
                $"\n <color=green> Jump action handled. </color> \n" +
                $"Airborn Jump Count: {AirbornJumpCount}");
        }

        private void HandleGroundedStateChanged(bool ctx) 
        { 
            if (ctx)
            {
                ResetJumpBufferTimer();
                AirbornJumpCount = 0;
            }
        }

        private void ResetCoyoteTimer() { CoyoteTimer = _movementConfig.JumpCoyoteTime; Debug.Log($"<color=yellow> [JumpBuffer] </color>" + "Coyote timer has been reset"); } 
        private void ResetJumpBufferTimer() { JumpBufferTimer = _movementConfig.JumpBufferTime; Debug.Log($"<color=yellow> [JumpBuffer] </color>" + "Buffer timer has been reset"); } 

        public void Dispose()
        {
            _movementInputProvider.OnJumpRequested -= ResetJumpBufferTimer;
            _groundedChecker.GroundedStateChanged -= ctx => HandleGroundedStateChanged(ctx);
        }
    }
}