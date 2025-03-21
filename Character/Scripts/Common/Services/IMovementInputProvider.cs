using System;

public interface IMovementInputProvider
{
    float MovementDirection { get; }
    bool IsSprinting {  get; } 
    event Action OnJumpRequested;
}
