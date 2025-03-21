using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public sealed class JumpState : IState
    {
        public bool InAction { get; private set; }

        private readonly MovementConfig _movementConfig;
        private readonly IJumpBuffer _jumpBuffer;
        private readonly IMovementHandler _movementHandler;
        private readonly IMovementInputProvider _inputProvider;

        private float _jumpStartTime;
        private float _maxJumpTime;

        public JumpState(StateMediator stateMediator, IMovementInputProvider inputProvider, IJumpBuffer jumpBuffer)
        {
            _jumpBuffer = jumpBuffer;
            _movementConfig = stateMediator.Config;
            _movementHandler = stateMediator.MovementHandler;
            _inputProvider = inputProvider;
        }

        public void Enter()
        {
            HandleJumpRequest();
            _inputProvider.OnJumpRequested += HandleJumpRequest;
        }

        public void Update()
        {
            if (!InAction) return;

            float jumpTime = Time.time - _jumpStartTime;

            if (jumpTime < _maxJumpTime)
                _movementHandler.Move(new Vector2(_inputProvider.MovementDirection * _movementConfig.AirbornSpeed, _movementConfig.JumpCurve.Evaluate(jumpTime)));
            else
                Break();
        }
        public void Break()
        {
            InAction = false;
            Debug.Log($"State {nameof(JumpState)} is no more in action");
        }

        public void Exit()
        {
            _inputProvider.OnJumpRequested -= HandleJumpRequest;
        }

        private void HandleJumpRequest()
        {
            if (_jumpBuffer.AirbornJumpCount >= _movementConfig.MaxAirbornJumpCount) return;
            _jumpStartTime = Time.time;
            _maxJumpTime = _movementConfig.JumpCurve.keys[_movementConfig.JumpCurve.keys.Length - 1].time;

            _jumpBuffer.HandleJump();

            InAction = true;
            Debug.Log($"State {nameof(JumpState)} is in action");
        }
    }
}