using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public sealed class IdleState : IState
    {
        public bool InAction { get; private set; }

        private readonly IMovementHandler _movementHandler;

        public IdleState(StateMediator stateMediator) { _movementHandler = stateMediator.MovementHandler; }

        public void Enter() => _movementHandler.Move(new Vector2 (0f, 0f));
    }
}