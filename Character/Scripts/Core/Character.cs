using System;
using System.Collections.Generic;
using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected MovementConfig _movementConfig;

        public event Action<Collider2D> TriggerEnter;
        public event Action<Collider2D> TriggerExit;

        protected IMovementInputProvider _movementInputProvider;
        protected IStateManager _stateManager;
        protected IMovementHandler _movementHandler;
        protected StateContainer _stateContainer;
        protected IGroundedChecker _groundedChecker;

        protected List<IDisposable> disposables = new List<IDisposable>();

        protected virtual void Awake()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            _movementHandler = new MovementHandler(rb);
            StateMediator stateMediator = new StateMediator(rb, GetComponent<Animator>(), _movementConfig, _movementHandler);
            GroundedChecker groundedComponent = new GroundedChecker(this, _movementConfig);

            _groundedChecker = groundedComponent;
            _stateContainer = new StateContainer(RegisterStates(stateMediator));
            _stateManager = new StateManager(_stateContainer, RegisterTransitions());

            disposables.Add(groundedComponent);
        }

        protected abstract Dictionary<Type, IState> RegisterStates(StateMediator mediator);

        protected abstract Transition[] RegisterTransitions();

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            TriggerEnter?.Invoke(collider);
        }
        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            TriggerExit?.Invoke(collider);
        }
        protected virtual void Update()
        {
            _stateManager.Update();
        }
        protected void OnDestroy()
        {
            foreach (var disposable in disposables)
                disposable.Dispose();
        }
    }
}