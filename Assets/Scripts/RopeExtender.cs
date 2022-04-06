using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi; 

public class RopeExtender : MonoBehaviour
{
    
    [Header("Rope Parts")]
    public ObiRope rope; 
    public ObiRopeCursor cursor; 

    [Header("Paramaters")]
    public float velocityCutoff;
    public float ropeAddIncrement; 

    private Rigidbody rb; 

    void Start() {
        rb = GetComponent<Rigidbody>(); 
	}

    void FixedUpdate() {
        if(rb.velocity.magnitude > velocityCutoff) {
            cursor.ChangeLength(rope.restLength + ropeAddIncrement * Time.fixedDeltaTime); 
	    }
	}
}
