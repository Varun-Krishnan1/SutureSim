using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleWoundState : StateInterface
{
    Needle needle; 

    SqueezableInstrument instrumentAttached; 

    public bool needleWoundRotation; 

    // private float previousInstrumentRotationY = 0;
    private Vector3 previousInstrumentRotation = Vector3.zero; 

    public NeedleWoundState(Needle needle) {
        this.needle = needle;


	}

     public void Enter() {
        Logger.Instance.AppendText("Entered wound state!");
      
        // needle.instrument.FreezePosition(); 

        /* 

        needle.transform.parent = null; 
        needle.instrument = null;
        needle.rb.constraints = RigidbodyConstraints.None; 
        needle.rb.useGravity = false; 
        
        needle.FreezeInWound(); 
        // -- Adding configurable joint so needle rotates around only one axis 
        ConfigurableJoint joint = needle.gameObject.AddComponent<ConfigurableJoint>(); 
        joint.connectedBody = needle.wound.rb; 

        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        //joint.angularXMotion = ConfigurableJointMotion.Locked;
        //joint.angularYMotion = ConfigurableJointMotion.Locked;
        //joint.angularZMotion = ConfigurableJointMotion.Locked;

        */ 
	}

    public void Execute() {
        if(needleWoundRotation) {
            if(previousInstrumentRotation != Vector3.zero) {
                // float instrumentDeltaY = instrument.transform.rotation.eulerAngles.y - previousInstrumentRotationY; 
                Vector3 diff = needle.instrument.transform.rotation.eulerAngles - previousInstrumentRotation; 

                needle.transform.eulerAngles += diff;
			}      
            // previousInstrumentRotationY = instrument.transform.rotation.eulerAngles.y; 
            previousInstrumentRotation = needle.transform.rotation.eulerAngles;            
		}
	}

    public void Exit() {
        Logger.Instance.AppendText("Exiting wound state!");
        needle.wound = null; 
	}

    public void InstrumentGrab(SqueezableInstrument instrument) {
        needleWoundRotation = true; 
        needle.instrument = instrument; 

        instrument.FreezePosition(); 
        /* 
        ConfigurableJoint joint = instrument.gameObject.AddComponent<ConfigurableJoint>(); 
        joint.connectedBody = needle.rb; 

        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        */
         
	}

    public void InstrumentDrop(SqueezableInstrument instrument) {
        needleWoundRotation = false; 
        needle.instrument = null; 
	}

}
