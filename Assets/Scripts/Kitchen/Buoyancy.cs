using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{

    private Rigidbody rb;
    private Transform soupPosition;
    
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        soupPosition = GameObject.FindObjectOfType<Waves>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get the real height of the soup
        float waveOffset = WaveManager.instance.GetWaveLength(transform.position.x, transform.position.z);
        if (transform.position.y < soupPosition.transform.position.y + waveOffset)
        {
            //if the gameobject is under the soup, apply an upward force 
            float displacementMultiplier = Mathf.Clamp01((waveOffset + soupPosition.transform.position.y - transform.position.y ) / depthBeforeSubmerged) * displacementAmount; 
            Vector3 force = -Physics.gravity * displacementMultiplier;
            rb.AddForce(force, ForceMode.Acceleration);
        }
    }
}
