using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDir;
    private float curSpeed;
    private float ySpeed;
    private bool walk;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(MoveRoutine());
        StartCoroutine(JumpRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnMove(InputValue input)
    {
        moveDir.x = input.Get<Vector2>().x;
        moveDir.z = input.Get<Vector2>().y;
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (moveDir.magnitude <= 0)
            {
                curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f);
                animator.SetFloat("MoveSpeed", curSpeed);
                yield return null;
                continue;
            }
                

            Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
            Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

            if (walk)
            {
                curSpeed = Mathf.Lerp(curSpeed, walkSpeed, 0.1f);
            }
            else
            {
                curSpeed = Mathf.Lerp(curSpeed, runSpeed, 0.1f);
            }


            controller.Move(moveDir.z * forwardVec * curSpeed * Time.deltaTime);
            controller.Move(moveDir.x * rightVec * curSpeed * Time.deltaTime);
            animator.SetFloat("MoveSpeed", curSpeed);
            Quaternion lookRotation = Quaternion.LookRotation(forwardVec * moveDir.z + rightVec * moveDir.x);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.05f);

            yield return null;
        }    
    }

    private void OnJump(InputValue input)
    {
        if (GroundCheck())
            ySpeed = jumpSpeed;
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (GroundCheck() && ySpeed < 0)
                ySpeed = -1;

            controller.Move(Vector3.up * ySpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnWalk(InputValue input)
    {
        walk = input.isPressed;
    }
    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.5f);
    }
    
}
