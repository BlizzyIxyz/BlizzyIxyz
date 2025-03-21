using System;

public class PlayerMovementInputProvider : IMovementInputProvider, IDisposable
{
    public float MovementDirection { get; private set; }
    public bool IsSprinting { get; private set; }

    public event Action OnJumpRequested;

    private InputBindings _inputBindings;

    public void Initialize()
    {
        _inputBindings = new InputBindings();
        _inputBindings?.Enable();
        RegisterInputEvents();
    }
    private void RegisterInputEvents()
    {
        _inputBindings.Player.Move.performed += ctx => MovementDirection = ctx.ReadValue<float>();
        _inputBindings.Player.Move.canceled += ctx => MovementDirection = 0f;

        _inputBindings.Player.Jump.started += ctx => OnJumpRequested?.Invoke();
        _inputBindings.Player.Sprint.performed += ctx => IsSprinting = true;
        _inputBindings.Player.Sprint.canceled += ctx => IsSprinting = false;
    }
    public void Dispose()
    {
        _inputBindings?.Disable();
        _inputBindings.Player.Jump.started -= ctx => OnJumpRequested?.Invoke();
    }
}