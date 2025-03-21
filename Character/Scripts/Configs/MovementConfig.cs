using UnityEngine;

namespace UNNAMEDGAME.Game.Character
{
    [CreateAssetMenu(menuName = "Configs/Movement config")]
    public class MovementConfig : ScriptableObject
    {
        [field: Header("Speed settings")]
        [field: SerializeField] public float WalkSpeed { get; private set; }
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float AirbornSpeed { get; private set; }

        [field: Space, Header("Jump settings")]
        [field: SerializeField] public AnimationCurve JumpCurve { get; private set; }
        [field: SerializeField] public int MaxAirbornJumpCount { get; private set; }
        [field: SerializeField] public float JumpCoyoteTime { get; private set; }
        [field: SerializeField] public float JumpBufferTime { get; private set; }

        [field: Space, Header("Grounded checker settings")]
        [field: SerializeField] public LayerMask[] GroundLayerMasks { get; private set; }
    }
}
