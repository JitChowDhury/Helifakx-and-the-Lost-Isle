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
        Debug.Log(controller.buttons.Count);
    }

    public override void SelectButton()
    {
        Button btn = controller.buttons[controller.currentSelection];
    }
}
