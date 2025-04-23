using UnityEngine;
namespace RPG.Utility
{
    public class BillBoard : MonoBehaviour
    {
        private GameObject cam;
        void Awake()
        {
            cam = GameObject.FindGameObjectWithTag(Constants.CAMERA_TAG);
        }

        void LateUpdate()
        {
            Vector3 cameraDirection = transform.position + cam.transform.forward;
            transform.LookAt(cameraDirection);

        }
    }
}
