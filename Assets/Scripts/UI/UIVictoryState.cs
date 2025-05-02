using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Utility;
using RPG.Core;

namespace RPG.UI
{
    public class UIVictoryState : UIBaseState
    {
        public UIVictoryState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAMEMANAGER_TAG
            ).GetComponent<PlayerInput>();
            VisualElement victoryContainer = controller.root
                .Q<VisualElement>("victory-container");

            playerInputCmp.SwitchCurrentActionMap(
                Constants.UI_ACTION_MAP
            );
            victoryContainer.style.display = DisplayStyle.Flex;

            controller.audioSourceCmp.clip = controller.VictoryAudio;
            controller.audioSourceCmp.Play();
            controller.canPause = false;
        }

        public override void SelectButton()
        {
            PlayerPrefs.DeleteAll();
            controller.StartCoroutine(SceneTransition.Initiate(0));
        }
    }
}