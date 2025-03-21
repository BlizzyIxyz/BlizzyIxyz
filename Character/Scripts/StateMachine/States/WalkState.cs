using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public sealed class WalkState : IState
    {
        public bool InAction { get; private set; }

        private readonly MovementConfig _movementConfig;
        private readonly IMovementHandler _movementHandler;
        private readonly Rigidbody2D _rb;
        private readonly IMovementInputProvider _inputProvider;

        public WalkState(StateMediator stateMediator, IMovementInputProvider inputProvider)
        {
            _movementConfig = stateMediator.Config;
            _movementHandler = stateMediator.MovementHandler;
            _rb = stateMediator.Rigidbody;
            _inputProvider = inputProvider;
        }

        public void Update()
        {
            _movementHandler.Move(new Vector2(_inputProvider.MovementDirection * _movementConfig.WalkSpeed, _rb.linearVelocityY));
        }
    }
}