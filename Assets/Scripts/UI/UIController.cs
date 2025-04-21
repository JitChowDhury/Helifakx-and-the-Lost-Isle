using System.Collections.Generic;
using RPG.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public UIBaseState currenState;
    public UIMainMenuState mainMenuState;

    private UIDocument uIDocumentCmp;
    public VisualElement root;
    public List<Button> buttons;

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

}
