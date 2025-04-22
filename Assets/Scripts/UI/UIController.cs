using System.Collections.Generic;
using RPG.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public UIBaseState currenState;
    public UIMainMenuState mainMenuState;

    private UIDocument uIDocumentCmp;
    public VisualElement root;
    public List<Button> buttons;
    public int currentSelection = 0;

    void Awake()
    {
        mainMenuState = new UIMainMenuState(this);
        uIDocumentCmp = GetComponent<UIDocument>();
        root = uIDocumentCmp.rootVisualElement;


    }

    void Start()
    {
        currenState = mainMenuState;
        currenState.EnterState();
    }
    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        currenState.SelectButton();
    }

    public void HandleNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        buttons[currentSelection].RemoveFromClassList("active");


        Vector2 input = context.ReadValue<Vector2>();
        currentSelection += input.x > 0 ? 1 : -1;
        currentSelection = Mathf.Clamp(currentSelection, 0, buttons.Count - 1);
        buttons[currentSelection].AddToClassList("active");

    }


}
