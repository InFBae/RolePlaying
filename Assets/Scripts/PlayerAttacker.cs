using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] bool debug;

    [SerializeField] int damage;
    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;

    private Animator animator;
    private bool isAttacking = false;
    private float cosResult;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //cosResult = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void OnAttack(InputValue input)
    {
        //Attack();
        if (!isAttacking)
        {
            attackRoutine = StartCoroutine(AttackRoutine());
        }
        
    }

    Coroutine attackRoutine;
    IEnumerator AttackRoutine()
    {
        if (isAttacking)
        {
            yield return null;
        }
        isAttacking = true;
        animator.SetLayerWeight(1, 1);
        Attack();
        yield return new WaitForSeconds(1f);
        animator.SetLayerWeight(1, 0);
        isAttacking = false;    
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void AttackTiming()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            IHittable hittable = collider.GetComponent<IHittable>();
            hittable?.TakeHit(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Debug.DrawRay(transform.position, rightDir * range, Color.yellow);
        Debug.DrawRay(transform.position, leftDir * range, Color.yellow);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0 , Mathf.Cos(radian));
    }
}
