using System.Collections.Generic;
using RPG.Core;
using RPG.Quest;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<QuestItemSO> items = new List<QuestItemSO>();
    private bool itemFound = false;

    void OnEnable()
    {
        EventManager.OnTreasureChestUnlocked += HandleQuestItemSO;
    }

    void OnDisable()
    {
        EventManager.OnTreasureChestUnlocked -= HandleQuestItemSO;
    }


    public void HandleQuestItemSO(QuestItemSO newItem, bool showUI)
    {
        items.Add(newItem);
    }

    public bool HasItem(QuestItemSO desiredItem)
    {
        itemFound = false;
        items.ForEach((QuestItemSO item) =>
        {
            if (desiredItem.name == item.name) itemFound = true;
        });

        return itemFound;
    }

}
