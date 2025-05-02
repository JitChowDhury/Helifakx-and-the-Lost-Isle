using RPG.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int nextSceneIndex;
        public Transform spawnPoint;
        private Collider colliderCmp;
        void Awake()
        {
            colliderCmp = GetComponent<Collider>();
        }
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            colliderCmp.enabled = false;

            EventManager.RaisePortalEnter(other, nextSceneIndex);
            StartCoroutine(SceneTransition.Initiate(nextSceneIndex));
        }
    }
}