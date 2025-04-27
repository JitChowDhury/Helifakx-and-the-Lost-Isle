using System.Collections.Generic;
using RPG.Core;
using RPG.Quest;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<QuestItemSO> items = new List<QuestItemSO>();

    void OnEnable()
    {
        EventManager.OnTreasureChestUnlocked += HandleQuestItemSO;
    }

    void OnDisable()
    {
        EventManager.OnTreasureChestUnlocked -= HandleQuestItemSO;
    }


    public void HandleQuestItemSO(QuestItemSO newItem)
    {
        items.Add(newItem);
    }

}
