using System;

namespace UNNAMEDGAME.Game.Character
{

    public class Transition
    {
        public readonly Type[] FromStates;
        public readonly Func<bool> Condition;
        public readonly Type ToState;

        public Transition(Type[] fromStates, Func<bool> condition, Type toState)
        {
            if (fromStates == null)
                throw new ArgumentNullException(nameof(fromStates), "fromStates cannot be null");

            foreach (var state in fromStates)
            {
                if (state == null)
                    throw new ArgumentNullException(nameof(state), "A state in fromStates cannot be null");

                if (!typeof(IState).IsAssignableFrom(state))
                    throw new InvalidOperationException($"fromStates must implement IState. {this}");
            }

            if (toState == null)
                throw new ArgumentNullException(nameof(toState), "toState cannot be null");

            if (!typeof(IState).IsAssignableFrom(toState))
                throw new InvalidOperationException($"toState must implement IState. {this}");

            FromStates = fromStates;
            Condition = condition;
            ToState = toState;
        }
    }
}