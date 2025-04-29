using UnityEngine;
namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        void OnEnable()
        {
            EventManager.OnPortalEnter += HandlePortalEnter;
        }
        void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlePortalEnter;

        }
        private void HandlePortalEnter(Collider player, int nextSceneIndex)
        {
            print("Portal Entered");
        }
    }
}
