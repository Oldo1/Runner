using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Game config", menuName = "Game config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _vSyncCount;
        [SerializeField] private int _targetFrameRate;

        public int VSyncCount => _vSyncCount;
        public int TargetFrameRate => _targetFrameRate;
    }
}
