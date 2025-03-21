using System;
using System.Collections.Generic;
using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    public class StateContainer
    {
        private readonly Dictionary<Type, IState> _cachedStates = new();

        public StateContainer(Dictionary<Type, IState> states)
        {
            foreach (var state in states)
                _cachedStates[state.Key] = state.Value;
        }

        public IState GetState(Type stateType)
        {
            if (_cachedStates.TryGetValue(stateType, out var state))
                return state;

            Debug.LogError($"State {stateType} not found. {this}");
            throw new InvalidOperationException($"State {stateType} not found. {this}");
        }
    }
}