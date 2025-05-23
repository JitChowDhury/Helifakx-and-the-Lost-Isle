using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Utility;
using RPG.Core;

namespace RPG.UI
{
    public class UIQuestItemState : UIBaseState
    {
        private VisualElement questItemContainer;
        private Label questItemText;
        private PlayerInput playerInputCmp;

        public UIQuestItemState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAMEMANAGER_TAG
            ).GetComponent<PlayerInput>();

            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
            EventManager.RaiseToggleUI(true);

            questItemContainer = controller.root.Q<VisualElement>("quest-item-container");
            questItemText = questItemContainer.Q<Label>("quest-item-label");

            questItemContainer.style.display = DisplayStyle.Flex;
            controller.canPause = false;
        }

        public override void SelectButton()
        {
            questItemContainer.style.display = DisplayStyle.None;
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);
            EventManager.RaiseToggleUI(false);
            controller.canPause = true;
        }

        public void SetQuestItemLabel(string name)
        {
            questItemText.text = name;
        }
    }
}