using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerFoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colidiu com algo");
        GetComponentInParent<Rigidbody>().AddForce(other.transform.position-transform.position * 1, ForceMode.Impulse);
    }
}
