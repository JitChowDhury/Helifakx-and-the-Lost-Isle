using UnityEngine;
using UnityEngine.Splines;

public class Patrol : MonoBehaviour
{
    [SerializeField] private GameObject splineGameobject;
    private SplineContainer splineCmp;
    void Awake()
    {
        if (splineGameobject == null)
        {
            Debug.LogWarning($"{name} does not have Spline");
        }

        splineCmp = splineGameobject.GetComponent<SplineContainer>();
    }

    public Vector3 GetNextPosition()
    {
        return splineCmp.EvaluatePosition(0);
    }
}
