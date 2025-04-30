using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "CreateModels/" + nameof(GameConfigData))]
    public sealed class GameConfigData : ScriptableObject
    {
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
        [field: SerializeField] public int StartingCoins { get; set; }
        [field: SerializeField] public int BaseHealth { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            StartingCoins = Mathf.Max(1, StartingCoins);
            BaseHealth = Mathf.Max(1, BaseHealth);
        }
#endif
    }
}