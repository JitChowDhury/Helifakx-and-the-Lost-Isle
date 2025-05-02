using System.Collections;
using UnityEngine.SceneManagement;
using RPG.Utility;
using UnityEngine;

namespace RPG.Core
{
    public static class SceneTransition
    {
        public static IEnumerator Initiate(int sceneIndex)
        {
            AudioSource audioSourceCmp = GameObject.FindGameObjectWithTag(Constants.GAMEMANAGER_TAG).GetComponent<AudioSource>();

            float duration = 2f;

            while (audioSourceCmp.volume > 0)
            {
                audioSourceCmp.volume -= Time.deltaTime / duration;

                yield return new WaitForEndOfFrame();
            }
            SceneManager.LoadScene(sceneIndex);



        }
    }
}
