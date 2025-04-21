namespace RPG.UI
{

    public abstract class UIBaseState
    {
        public UIController controller;

        public UIBaseState(UIController uI)
        {
            controller = uI;
        }

        public abstract void EnterState();
    public abstract void SelectButton();
    }


}