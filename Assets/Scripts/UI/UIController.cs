using System;
using System.Collections.Generic;
using RPG.Core;
using RPG.Quest;
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
    public UIVictoryState victoryState;
    public UIGameOverState gameOverState;
    public UIPauseState pauseState;
    public UIUnpauseState unpauseState;
    public AudioClip GameOverAudio;
    public AudioClip VictoryAudio;
    [NonSerialized] public AudioSource audioSourceCmp;

    private UIDocument uIDocumentCmp;
    public VisualElement root;

    public VisualElement mainMenuContainer;
    public VisualElement playerInfoContainer;
    public VisualElement questItem;
    public Label healthLabel;
    public Label potionLabel;
    public List<Button> buttons = new List<Button>();
    public int currentSelection = 0;
    public bool canPause = true;



    void Awake()
    {

        mainMenuState = new UIMainMenuState(this);
        dialogueState = new UIDialogueState(this);
        questItemState = new UIQuestItemState(this);
        victoryState = new UIVictoryState(this);
        gameOverState = new UIGameOverState(this);
        pauseState = new UIPauseState(this);
        unpauseState = new UIUnpauseState(this);


        uIDocumentCmp = GetComponent<UIDocument>();
        audioSourceCmp = GetComponent<AudioSource>();
        root = uIDocumentCmp.rootVisualElement;
        playerInfoContainer = root.Q<VisualElement>("player-info-container");
        mainMenuContainer = root.Q<VisualElement>("main-menu-container");
        healthLabel = playerInfoContainer.Q<Label>("health-label");
        potionLabel = playerInfoContainer.Q<Label>("potions-label");
        questItem = playerInfoContainer.Q<VisualElement>("quest-item-icon");




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
        EventManager.OnVictory += HandleVictory;
        EventManager.OnGameOver += HandleGameOver;
    }

    void OnDisable()
    {
        EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
        EventManager.OnChangePotionsCount -= HandleChangePotionCount;
        EventManager.OnInitiateDialogue -= HandleInitiateDialogue;
        EventManager.OnTreasureChestUnlocked -= HandleTreasureChestUnlocked;
        EventManager.OnVictory -= HandleVictory;
        EventManager.OnGameOver -= HandleGameOver;


    }
    public void HandleInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;


        currentState.SelectButton();
    }

    public void HandleNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed || buttons.Count == 0) return;

        buttons[currentSelection].RemoveFromClassList("active");

        Vector2 input = context.ReadValue<Vector2>();
        currentSelection += input.x > 0 ? 1 : -1;
        currentSelection = Mathf.Clamp(
            currentSelection, 0, buttons.Count - 1
        );

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

    private void HandleInitiateDialogue(TextAsset inkJSON, GameObject NPC)
    {
        currentState = dialogueState;
        currentState.EnterState();
        (currentState as UIDialogueState).SetStory(inkJSON, NPC);



    }

    private void HandleTreasureChestUnlocked(QuestItemSO item, bool showUI)
    {
        questItem.style.display = DisplayStyle.Flex;
        if (!showUI) return;

        currentState = questItemState;
        currentState.EnterState();
        (currentState as UIQuestItemState).SetQuestItemLabel(item.itemName);

    }
    private void HandleVictory()
    {
        currentState = victoryState;
        currentState.EnterState();

    }

    private void HandleGameOver()
    {
        currentState = gameOverState;
        currentState.EnterState();
    }

    public void HandlePause(InputAction.CallbackContext context)
    {
        if (!context.performed || !canPause)
        {
            return;
        }
        currentState = currentState == pauseState ? unpauseState : pauseState;
        currentState.EnterState();
    }

}
