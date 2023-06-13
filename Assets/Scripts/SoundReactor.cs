using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReactor : MonoBehaviour, IListenable
{
    Coroutine lookAtRoutine;
    public void Listen(Transform trans)
    {
        //transform.LookAt(trans.position);
        if (lookAtRoutine != null)
        {
            StopCoroutine(lookAtRoutine);
        }
        lookAtRoutine = StartCoroutine(LookAtRoutine(trans.position));       
    }

    IEnumerator LookAtRoutine(Vector3 dir)
    {
        
        while (true)
        {
            Vector3 targetDir = (dir - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 3f * Time.deltaTime);
            yield return null;

            if(Vector3.Dot(transform.forward, targetDir) > 0.99)
            {
                yield break;
            }
        }       
        
    }
}
