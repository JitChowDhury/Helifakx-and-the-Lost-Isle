using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.UI
{
    public class UIPauseState : UIBaseState
    {
        public UIPauseState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAMEMANAGER_TAG
            ).GetComponent<PlayerInput>();
            VisualElement pauseContainer = controller.root
                .Q<VisualElement>("pause-container");

            playerInputCmp.SwitchCurrentActionMap(
                Constants.UI_ACTION_MAP
            );
            pauseContainer.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }

        public override void SelectButton() { }
    }
}