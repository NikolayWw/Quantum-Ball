using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player Static Data", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public GameObject PlayerBallPrefab { get; private set; }
        
        [field: SerializeField] public float DeflateSpeed { get; private set; } = 1;
        [field: SerializeField] public float PlayerBallMoveSpeed { get; private set; } = 10;
        [field: SerializeField] public float DeflateSizeToLose  { get; private set; } = 1;
        [field: SerializeField] public int MaxShootCount  { get; private set; } = 3;
        
        [field: SerializeField] public float SecondsForMaxInfectionRadius { get; private set; } = 2;
        [field: SerializeField] public float MaxInfectionRadius { get; private set; } = 4;

        [field: SerializeField] public float WinJumpForce { get; private set; } = 2;
        [field: SerializeField] public float WinForwardMoveSpeed { get; private set; } = 2;
    }
}