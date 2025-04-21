using RPG.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIBaseState currenState;
    public UIMainMenuState mainMenuState;

    void Awake()
    {
        mainMenuState = new UIMainMenuState(this);


    }

    void Start()
    {
        currenState = mainMenuState;
        currenState.EnterState();
    }

}
