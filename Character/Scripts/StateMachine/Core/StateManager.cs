using System;
using UnityEngine;
using System.Linq;

namespace UNNAMEDGAME.Game.Character
{
    public sealed class StateManager : IStateManager
    {
        public IState CurrentState { get; private set; }

        private readonly StateContainer _stateContainer;

        private readonly Transition[] _transitions;

        public event Action<IState> StateChanged;

        public StateManager(StateContainer stateContainer, Transition[] transitions)
        {
            _stateContainer = stateContainer;
            _transitions = transitions;

            CurrentState = _stateContainer.GetState(typeof(IdleState));
        }

        public void Update()
        {
            CheckTransitions();
            CurrentState.Update();
        }

        private void CheckTransitions()
        {
            foreach (var transition in _transitions)
            {
                if (transition.Condition())
                    if (transition.ToState == CurrentState.GetType())
                        return;
                    else if (transition.FromStates.Contains(CurrentState.GetType()))
                    {
                        IState toState = _stateContainer.GetState(transition.ToState);
                        TransitionTo(toState);
                        return;
                    }
            }
        }

        private void TransitionTo(IState state)
        {
            string previousStateName = CurrentState?.GetType().Name ?? "None";
            string newStateName = state?.GetType().Name ?? "None";

            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();

            StateChanged?.Invoke(state);

            Debug.Log($"<color=yellow> [StateMachine] </color>" +  $"Transition: {previousStateName} → {newStateName}");
        }
    }
}