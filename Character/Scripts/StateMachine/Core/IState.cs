namespace UNNAMEDGAME.Game.Character
{
    public interface IState
    {
        bool InAction { get; }
        void Enter() { }
        void Update() { }
        void Break() { }
        void Exit() { }
    }
}