using System.Collections.Generic;
using RPG.Core;
using RPG.UI;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public UIBaseState currentState;
    public UIMainMenuState mainMenuState;
    public UIDialogueState dialogueState;
    public UIQuestItemState questItemState;

    private UIDocument uIDocumentCmp;
    public VisualElement root;

    public VisualElement mainMenuContainer;
    public VisualElement playerInfoContainer;
    public Label healthLabel;
    public Label potionLabel;
    public List<Button> buttons = new List<Button>();
    public int currentSelection = 0;


   
    void Awake()
    {
        mainMenuState = new UIMainMenuState(this);
        dialogueState = new UIDialogueState(this);
        questItemState = new UIQuestItemState(this);

        uIDocumentCmp = GetComponent<UIDocument>();
        root = uIDocumentCmp.rootVisualElement;
        playerInfoContainer = root.Q<VisualElement>("player-info-container");
        mainMenuContainer = root.Q<VisualElement>("main-menu-container");
        healthLabel = playerInfoContainer.Q<Label>("health-label");
        potionLabel = playerInfoContainer.Q<Label>("potions-label");



    }

    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            currentState = mainMenuState;
            currentState.EnterState();
        }
        else
        {
            playerInfoContainer.style.display = DisplayStyle.Flex;
        }


    }

    void OnEnable()
    {
        EventManager.OnChangePlayerHealth += HandleChangePlayerHealth;
        EventManager.OnChangePotionsCount += HandleChangePotionCount;
        EventManager.OnInitiateDialogue += HandleInitiateDialogue;
        EventManager.OnTreasureChestUnlocked += HandleTreasureChestUnlocked;
    }

    void OnDisable()
    {
        EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
        EventManager.OnChangePotionsCount -= HandleChangePotionCount;
        EventManager.OnInitiateDialogue -= HandleInitiateDialogue;
         EventManager.OnTreasureChestUnlocked -= HandleTreasureChestUnlocked;


    }
    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
       

        currentState.SelectButton();
    }

    public void HandleNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;


        Vector2 input = context.ReadValue<Vector2>();
        buttons[currentSelection].RemoveFromClassList("active");
        currentSelection += input.x > 0 ? 1 : -1;
        currentSelection = Mathf.Clamp(currentSelection, 0, buttons.Count - 1);
        buttons[currentSelection].AddToClassList("active");

    }

    private void HandleChangePlayerHealth(float newHealthPoints)
    {
        healthLabel.text = newHealthPoints.ToString();
    }

    private void HandleChangePotionCount(int newPotionCount)
    {
        potionLabel.text = newPotionCount.ToString();
    }

    private void HandleInitiateDialogue(TextAsset inkJSON)
    {
        currentState = dialogueState;
        currentState.EnterState();
        (currentState as UIDialogueState).SetStory(inkJSON);


        
    }

    private void HandleTreasureChestUnlocked()
    {
        
        currentState = questItemState;
        currentState.EnterState();

    }

  

}
