using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class CharacterAnimator : MonoBehaviour
    {
        const float LocomotionSmoothTime = 0.1f;
        Animator animator;
        NavMeshAgent agent;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            float speedPercent = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("speedPercent", speedPercent, LocomotionSmoothTime, Time.deltaTime);
        }
    }
}
