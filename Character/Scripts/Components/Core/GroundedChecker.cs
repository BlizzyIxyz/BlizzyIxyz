using System;
using System.Linq;
using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public sealed class GroundedChecker : IGroundedChecker, IDisposable
    {
        public bool IsGrounded { get; private set; }
        public event Action<bool> GroundedStateChanged;

        private LayerMask[] _groundLayers;
        private Character _character;

        public GroundedChecker(Character character, MovementConfig movementConfig)
        {
            _groundLayers = movementConfig.GroundLayerMasks;
            _character = character;

            _character.TriggerEnter += ctx => HandleTriggerEnter(ctx);
            _character.TriggerExit += ctx => HandleTriggerExit(ctx);
        }

        private void HandleTriggerEnter(Collider2D collider)
        {
            if (_groundLayers.Any(layerMask => (layerMask.value & (1 << collider.gameObject.layer)) != 0)
                && !IsGrounded)
            {
                GroundedStateChanged?.Invoke(true);
                IsGrounded = true;
                Debug.Log($"<color=yellow> [GroundedChecker] </color>" + $"Is grounded: {IsGrounded}");
            }
        }
        private void HandleTriggerExit(Collider2D collider)
        {
            if (_groundLayers.Any(layerMask => (layerMask.value & (1 << collider.gameObject.layer)) != 0)
                && IsGrounded)
            {
                GroundedStateChanged?.Invoke(false);
                IsGrounded = false;
                Debug.Log($"<color=yellow> [GroundedChecker] </color>" + $"Is grounded: {IsGrounded}");
            }
        }
        public void Dispose()
        {
            _character.TriggerEnter -= ctx => HandleTriggerEnter(ctx);
            _character.TriggerExit -= ctx => HandleTriggerExit(ctx);
        }
    }
}