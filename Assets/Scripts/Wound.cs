using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound : MonoBehaviour
{
    private Needle needle; 
    public Rigidbody rb; 

    private bool inIncision = false; 

    void Start() {
        rb = GetComponent<Rigidbody>(); 
	}

    void OnTriggerEnter(Collider other) {
        NeedleCollider needleCollider = other.GetComponent<NeedleCollider>();
        if(needleCollider) {
            if(needleCollider.isNeedleTip()) {
                needle = needleCollider.GetNeedle(); 

                // -- eventualy add enterWound() and exitWound() functions on needle state?  
                if(needle.stateMachine.currentState != needle.woundState) {
                    needle.wound = this; 
                    needle.ChangeState(needle.woundState); 
				}

			}
		}
	}

    /*
    void BeginIncision(Needle needleColliderNeedle) {
        if(!inIncision) {
            Debug.Log("Incision Started!");
            inIncision = true; 

            this.needle = needleColliderNeedle; 

            
            // -- Adding configurable joint so needle rotates around only one axis 
            ConfigurableJoint joint = needle.gameObject.AddComponent<ConfigurableJoint>(); 
            joint.connectedBody = rb; 

            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;
            joint.angularXMotion = ConfigurableJointMotion.Locked;
            joint.angularYMotion = ConfigurableJointMotion.Locked;
            joint.angularZMotion = ConfigurableJointMotion.Locked;
            
		}
        

	}
    */
}
