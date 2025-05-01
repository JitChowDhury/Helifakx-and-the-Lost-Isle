using UnityEngine;
using UnityEngine.Playables;
using RPG.Utility;
using Unity.VisualScripting;

namespace RPG.Core
{
    public class CinematicController : MonoBehaviour
    {
        PlayableDirector playableDirectorCmp;
        Collider colliderCmp;
        void Awake()
        {
            playableDirectorCmp = GetComponent<PlayableDirector>();
            colliderCmp = GetComponent<Collider>();
        }
        void Start()
        {
            colliderCmp.enabled = !PlayerPrefs.HasKey("SceneIndex");
        }

        void OnEnable()
        {
            playableDirectorCmp.played += HandlePlayed;
            playableDirectorCmp.stopped += HandleStopped;
        }


        void OnDisable()
        {
            playableDirectorCmp.played -= HandlePlayed;
            playableDirectorCmp.stopped -= HandleStopped;
        }
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;
            playableDirectorCmp.Play();
            colliderCmp.enabled = false;

        }

        private void HandlePlayed(PlayableDirector pd)
        {
            EventManager.RaiseCutSceneUpdated(false);
        }

        private void HandleStopped(PlayableDirector pd)
        {
            EventManager.RaiseCutSceneUpdated(true);
        }
    }

}