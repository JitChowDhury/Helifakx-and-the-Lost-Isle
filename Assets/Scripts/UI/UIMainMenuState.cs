using RPG.Core;
using RPG.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMainMenuState : UIBaseState
{

    public UIController ui;
    private int sceneIndex;
    public UIMainMenuState(UIController uI) : base(uI)
    {

    }
    public override void EnterState()
    {
        if (PlayerPrefs.HasKey("SceneIndex"))
        {
            sceneIndex = PlayerPrefs.GetInt("SceneIndex");
            AddButton();
        }
        controller.mainMenuContainer.style.display = DisplayStyle.Flex;
        controller.buttons = controller.mainMenuContainer.Query<Button>(null, "menu-button").ToList();
        controller.buttons[0].AddToClassList("active");

    }

    public override void SelectButton()
    {
        Button btn = controller.buttons[controller.currentSelection];
        if (btn.name == "start-button")
        {
            PlayerPrefs.DeleteAll();
            controller.StartCoroutine(SceneTransition.Initiate(1));

        }
        else
        {
            controller.StartCoroutine(SceneTransition.Initiate(sceneIndex));
        }
    }

    private void AddButton()
    {
        Button continueButton = new Button();
        continueButton.AddToClassList("menu-button");
        continueButton.text = "Continue";
        VisualElement mainMenuButtons = controller.mainMenuContainer.Q<VisualElement>("buttons");
        mainMenuButtons.Add(continueButton);



    }
}
