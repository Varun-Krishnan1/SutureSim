using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleAttachedState : StateInterface
{
    Needle needle; 

    public NeedleAttachedState(Needle needle) {
        this.needle = needle;
	}

    public void Enter() {
        Logger.Instance.AppendText("Entered attached state!");
        
        needle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        needle.gameObject.transform.parent = needle.instrument.transform; 
	}

    public void Execute() {
        
	}

    public void Exit() {
        Logger.Instance.AppendText("Exiting attached state!");
	}

    public void InstrumentGrab(SqueezableInstrument instrument) {
        // -- switching from one instrument to another 
        needle.instrument = instrument; // -- used for attached state to be able to access 
        needle.ChangeState(needle.attachedState); 
	}

    public void InstrumentDrop(SqueezableInstrument instrument) {
        // -- if it hasn't yet been transferred to another instrument 
        if(needle.gameObject.transform.parent == instrument.transform) {
            needle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; 

            needle.ChangeState(needle.unattachedState); 
		}
        // -- if already transferred do nothing 
		// PASS  
	}
}
