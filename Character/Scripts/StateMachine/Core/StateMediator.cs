using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public sealed class StateMediator
    {
        public readonly IMovementHandler MovementHandler;
        public readonly MovementConfig Config;

        public readonly Rigidbody2D Rigidbody;
        public readonly Animator Animator;

        public StateMediator(
            Rigidbody2D rigidbody,
            Animator animator,
            MovementConfig config,
            IMovementHandler movementHandler)
        {
            Rigidbody = rigidbody;
            Animator = animator;

            MovementHandler = movementHandler;
            Config = config;
        }
    }
}