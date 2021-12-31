
using System;
using UnityEngine;

public class KillingZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.collider.gameObject.GetComponent<Controller>().GameOver();
    }
}
