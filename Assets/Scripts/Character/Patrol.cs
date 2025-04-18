using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class Patrol : MonoBehaviour
{
    [SerializeField] private GameObject splineGameobject;
    [SerializeField] private float walkDuration = 3f;
    [SerializeField] private float pauseDuration = 2f;
    private SplineContainer splineCmp;
    private NavMeshAgent agent;
    private float splinePosition = 0f;
    private float splineLength = 0f;
    private float lengthWalked = 0f;
    private float walkTime = 0f;
    private float pauseTime = 0f;
    private bool isWalking = true;

    void Awake()
    {
        if (splineGameobject == null)
        {
            Debug.LogWarning($"{name} does not have Spline");
        }

        splineCmp = splineGameobject.GetComponent<SplineContainer>();
        splineLength = splineCmp.CalculateLength();
        agent = GetComponent<NavMeshAgent>();
    }

    public Vector3 GetNextPosition()
    {
        return splineCmp.EvaluatePosition(splinePosition);
    }

    public void CalculateNextPosition()
    {
        walkTime += Time.deltaTime;
        if (walkTime > walkDuration)
        {
            isWalking = false;
        }

        if (!isWalking)
        {
            pauseTime += Time.deltaTime;
            if (pauseTime < pauseDuration)
            {
                return;
            }
            ResetTimers();
        }
        lengthWalked += Time.deltaTime * agent.speed;
        if (lengthWalked > splineLength)
        {
            lengthWalked = 0f;
        }
        splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
    }

    public void ResetTimers()
    {
        pauseTime = 0f;
        walkTime = 0f;
        isWalking = true;
    }
    public Vector3 GetFartherDistance()
    {
        //for better rotation
        float tempSplinePosition = splinePosition + 0.02f;
        if (tempSplinePosition >= 1)
        {
            tempSplinePosition -= 1;
        }
        return splineCmp.EvaluatePosition(tempSplinePosition);
    }
}
