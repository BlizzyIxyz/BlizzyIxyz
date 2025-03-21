using System;

namespace UNNAMEDGAME.Game.Character
{
    public interface IStateManager
    {
        IState CurrentState { get; }
        event Action<IState> StateChanged;
        void Update();
    }
}