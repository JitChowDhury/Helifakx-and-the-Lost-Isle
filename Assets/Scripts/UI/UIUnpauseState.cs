using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.UI
{
    public class UIUnpauseState : UIBaseState
    {
        public UIUnpauseState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAMEMANAGER_TAG
            ).GetComponent<PlayerInput>();
            VisualElement pauseContainer = controller.root
                .Q<VisualElement>("pause-container");

            playerInputCmp.SwitchCurrentActionMap(
                Constants.GAMEPLAY_ACTION_MAP
            );
            pauseContainer.style.display = DisplayStyle.None;
            Time.timeScale = 1;
        }

        public override void SelectButton() { }
    }
}