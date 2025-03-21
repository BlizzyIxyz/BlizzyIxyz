using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public class MovementHandler : IMovementHandler
    {
        private Rigidbody2D _rb;
        public MovementHandler(Rigidbody2D rb)
        {
            _rb = rb;
        }
        public void Move(Vector2 direction)
        {
            _rb.linearVelocity = direction;
        }
    }
}
