using UnityEngine;

namespace RPG.UI
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float speed;
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, Time.deltaTime * speed, 0);
        }
    }
}
