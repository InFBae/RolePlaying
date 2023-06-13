using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] bool debug;
    public GameObject target;

    [SerializeField] float range;
    [SerializeField, Range(0f, 360f)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    private void Update()
    {
        FindTarget();
    }

    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        foreach (Collider collider in colliders)
        {
            Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;

            float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position, dirTarget, distanceToTarget, obstacleMask))
                continue;

            Debug.DrawRay(transform.position, dirTarget * distanceToTarget, Color.red);
            
            target = collider.gameObject;
            return;
        }
        target = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug) 
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);

        Debug.DrawRay(transform.position, lookDir * range, Color.green);
        Debug.DrawRay(transform.position, rightDir * range, Color.blue);
        Debug.DrawRay(transform.position, leftDir * range, Color.blue);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
