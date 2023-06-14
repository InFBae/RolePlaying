using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HittableAdaptor : MonoBehaviour, IHittable
{
    public UnityEvent<int> OnDamaged;

    public void TakeHit(int damage)
    {
        OnDamaged?.Invoke(damage);
    }
}
