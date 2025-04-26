using UnityEngine;
namespace RPG.Quest
{
    [CreateAssetMenu(fileName = "Quest Item", menuName = "RPG/Quest Item", order = 1)]
    public class QuestItemSO : ScriptableObject
    {
        [Tooltip("Item name must be unique to prevent conflicts with other quest items.")]
        public string itemName;
    }
}
