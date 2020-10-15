using UnityEngine;
using System.Collections;

public class HammerHit : MonoBehaviour
{
    public Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("HammerHit");
        }
    }
}
