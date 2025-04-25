using System.Collections.Generic;
using Ink.Runtime;
using RPG.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;
        private Story currentStory;
        private PlayerInput playerInputCmp;
        private bool hasChoices;

        public UIDialogueState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = controller.root.Q<VisualElement>("dialogue-container");
            dialogueText = dialogueContainer.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<VisualElement>("dialogue-next-button");
            choicesGroup = dialogueContainer.Q<VisualElement>("choices-group");

            playerInputCmp = GameObject.FindWithTag(Constants.GAMEMANAGER_TAG).GetComponent<PlayerInput>();
            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);

            dialogueContainer.style.display = DisplayStyle.Flex;
        }


        public override void SelectButton()
        {
            UpdateDialogue();
        }
        public void SetStory(TextAsset inkJSON)
        {
            currentStory = new Story(inkJSON.text);
            //UpdateDialogue();
        }
        public void UpdateDialogue()
        {
            dialogueText.text = currentStory.Continue();
            hasChoices = currentStory.currentChoices.Count > 0;
            if (hasChoices)
            {
                HandleNewChoices(currentStory.currentChoices);
            }
            else
            {
                nextButton.style.display = DisplayStyle.Flex;
                choicesGroup.style.display = DisplayStyle.None;
            }


        }

        private void HandleNewChoices(List<Choice> choice)
        {
            nextButton.style.display = DisplayStyle.None;
            choicesGroup.style.display = DisplayStyle.Flex;

            choicesGroup.Clear();
            controller.buttons?.Clear();
        }

    }
}