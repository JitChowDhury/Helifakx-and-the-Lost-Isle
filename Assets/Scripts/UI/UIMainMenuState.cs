using RPG.Core;
using RPG.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMainMenuState : UIBaseState
{

    public UIController ui;

    public UIMainMenuState(UIController uI) : base(uI)
    {

    }
    public override void EnterState()
    {
        controller.buttons = controller.root.Query<Button>(null, "menu-button").ToList();
        controller.buttons[0].AddToClassList("active");
        Debug.Log(controller.buttons.Count);
    }

    public override void SelectButton()
    {
        Button btn = controller.buttons[controller.currentSelection];
        if (btn.name == "start-button")
        {
            SceneTransition.Initiate(1);
        }
    }
}
