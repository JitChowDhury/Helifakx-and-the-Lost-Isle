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
        private Animator animator;
        private NavMeshAgent agent;
        private Vector3 movementVector;

        public float walkSpeed = 2f;//added
        public float runSpeed = 3f;//added
        [NonSerialized] public Vector3 originalForwardVector;
        [NonSerialized] public bool isMoving;
        [NonSerialized] public bool isRunning;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            originalForwardVector = transform.forward;
        }
        void Start()
        {
            if (animator == null)
            {
                Debug.Log("Animator not found");
            }
            agent.updateRotation = false;
        }
        void Update()
        {
            MovePlayer();
            MovementAnimator();
            if (CompareTag(Constants.PLAYER_TAG)) Rotate(movementVector);
        }

        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            if (context.performed) isMoving = true;
            if (context.canceled) isMoving = false;
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);

        }

        public void HandleRun(InputAction.CallbackContext context)//added
        {
            if (context.started)
            {
                agent.speed = runSpeed;
                isRunning = true;
            }
            if (context.canceled)
            {
                agent.speed = walkSpeed;

                isRunning = false;

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
            isMoving = true;
        }

        public void StopMovingAgent()
        {
            agent.ResetPath();
            isMoving = false;
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
            isMoving = true;
        }

        public void UpdateAgentSpeed(float newSpeed)
        {
            agent.speed = newSpeed;
        }
        private void MovementAnimator()
        {
            float smoothening = Time.deltaTime * agent.acceleration;
            float speed = animator.GetFloat(Constants.SPEED_ANIMATOR_PARAM);
            if (isMoving)
            {
                speed += smoothening;
            }
            else speed -= smoothening;

            if (isRunning && isMoving)
                speed = Mathf.Clamp(speed, 0.5f, 1);
            else if (isMoving)
                speed = Mathf.Clamp(speed, 0, 0.5f);
            else
                speed = Mathf.Clamp(speed, 0f, speed);
            animator.SetFloat(Constants.SPEED_ANIMATOR_PARAM, speed);
        }

    }

}


