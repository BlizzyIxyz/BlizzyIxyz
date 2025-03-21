using System;
using System.Collections.Generic;
using UnityEngine;

namespace UNNAMEDGAME.Game.Character.Player
{
    public sealed class PlayerCharacter : Character
    {
        private IJumpBuffer _jumpBuffer;

        protected override void Awake()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            _movementHandler = new MovementHandler(rb);
            StateMediator stateMediator = new StateMediator(rb, GetComponent<Animator>(), _movementConfig, _movementHandler);

            GroundedChecker groundedChecker = new GroundedChecker(this, _movementConfig);
            PlayerMovementInputProvider movementInputProvider = new PlayerMovementInputProvider();

            movementInputProvider.Initialize();

            _groundedChecker = groundedChecker;
            _movementInputProvider = movementInputProvider;

            _jumpBuffer = new JumpBuffer(_movementConfig, _groundedChecker, _movementInputProvider);
            _stateContainer = new StateContainer(RegisterStates(stateMediator));
            _stateManager = new StateManager(_stateContainer, RegisterTransitions());

            disposables.Add(groundedChecker);
            disposables.Add(movementInputProvider);
        }
        protected override void Update()
        {
            base.Update();
            _jumpBuffer.UpdateTimers();
        }
        protected override Transition[] RegisterTransitions()
        {
            return new Transition[]
            {
            new Transition(
                new Type[] { typeof(IdleState), typeof(RunState), typeof(WalkState), typeof(AirbornState) },
                () => (_jumpBuffer.AirbornJumpCount < _movementConfig.MaxAirbornJumpCount && _jumpBuffer.CoyoteTimer > 0) || (_stateManager.CurrentState.GetType() == typeof(JumpState) && _stateManager.CurrentState.InAction),
                typeof(JumpState)),
            new Transition(
                new Type[] { typeof(IdleState), typeof(JumpState), typeof(RunState), typeof(WalkState) },
                () => !_groundedChecker.IsGrounded,
                typeof(AirbornState)),
            new Transition(
                new Type[] { typeof(IdleState), typeof(WalkState), typeof(JumpState), typeof(AirbornState) },
                () => Math.Abs(_movementInputProvider.MovementDirection) > 0.01f && _groundedChecker.IsGrounded && _movementInputProvider.IsSprinting,
                typeof(RunState)),
            new Transition(
                new Type[] { typeof(IdleState), typeof(RunState), typeof(JumpState), typeof(AirbornState) },
                () => Math.Abs(_movementInputProvider.MovementDirection) > 0.01f && _groundedChecker.IsGrounded,
                typeof(WalkState)),
            new Transition(
                new Type[] { typeof(AirbornState), typeof(JumpState), typeof(RunState), typeof(WalkState) },
                () => _groundedChecker.IsGrounded,
                typeof(IdleState))
            };
        }

        protected override Dictionary<Type, IState> RegisterStates(StateMediator mediator)
        {
            return new Dictionary<Type, IState>()
            {
                { typeof(AirbornState), new AirbornState(mediator, _movementInputProvider) },
                { typeof(JumpState), new JumpState(mediator, _movementInputProvider, _jumpBuffer) },
                { typeof(WalkState), new WalkState(mediator, _movementInputProvider) },
                { typeof(RunState), new RunState(mediator, _movementInputProvider) },
                { typeof(IdleState), new IdleState(mediator) }
            };
        }
    }
}