using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Utility;
using RPG.Core;

namespace RPG.UI
{
    public class UIGameOverState : UIBaseState
    {
        public UIGameOverState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAMEMANAGER_TAG
            ).GetComponent<PlayerInput>();

            VisualElement gameOverContainer = controller.root
                .Q<VisualElement>("game-over-container");

            playerInputCmp.SwitchCurrentActionMap(
                Constants.UI_ACTION_MAP
            );
            gameOverContainer.style.display = DisplayStyle.Flex;
        }

        public override void SelectButton()
        {
            PlayerPrefs.DeleteAll();
            SceneTransition.Initiate(0);
        }
    }
}