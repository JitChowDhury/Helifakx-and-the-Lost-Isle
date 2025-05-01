using UnityEngine;
using UnityEngine.Playables;
using RPG.Utility;

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
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;
            playableDirectorCmp.Play();
            colliderCmp.enabled = false;

        }
    }

}