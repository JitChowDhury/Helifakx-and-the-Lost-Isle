using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Unity.Mathematics;
using RPG.Utility;
using UnityEngine.EventSystems;
using System;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Vector3 movementVector;
        public float walkSpeed = 2f;//added
        public float runSpeed = 3f;//added
        [NonSerialized] public Vector3 originalForwardVector;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            originalForwardVector = transform.forward;
        }
        void Start()
        {
            agent.updateRotation = false;
        }
        void Update()
        {
            MovePlayer();
            if (CompareTag(Constants.PLAYER_TAG)) Rotate(movementVector);
        }

        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);

        }

        public void HandleRun(InputAction.CallbackContext context)//added
        {
            if (context.started)
            {
                agent.speed = runSpeed;
                Debug.Log("Running");
            }
            if (context.canceled)
            {
                agent.speed = walkSpeed;
                Debug.Log("Walking");

            }

        }

        public void Rotate(Vector3 newMovementVector)
        {
            if (newMovementVector == Vector3.zero) return;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(newMovementVector);

            transform.rotation = Quaternion.Lerp(startRotation, endRotation, Time.deltaTime * agent.angularSpeed);
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void StopMovingAgent()
        {
            agent.ResetPath();
        }

        public bool ReachedDestination()
        {
            if (agent.pathPending) return false;
            if (agent.remainingDistance > agent.stoppingDistance) return false;
            if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;


            return true;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            agent.Move(offset);
        }

        public void UpdateAgentSpeed(float newSpeed)
        {
            agent.speed = newSpeed;
        }

    }

}


