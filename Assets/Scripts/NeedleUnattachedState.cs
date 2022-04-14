using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleUnattachedState : StateInterface
{
    Needle needle; 

    public NeedleUnattachedState(Needle needle) {
        this.needle = needle; 
	}

    public void Enter() {
        Logger.Instance.AppendText("Entered unattached state!");

        needle.transform.parent = null; 
        needle.instrument = null; 
	}

    public void Execute() {
        
	}

    public void Exit() {
        Logger.Instance.AppendText("Exiting unattached state!");
	}

    public void InstrumentGrab(SqueezableInstrument instrument) {
        // -- since unattached switch to attached state 
        needle.instrument = instrument; // -- used for attached state to be able to access 
        needle.ChangeState(needle.attachedState); 
	}

    public void InstrumentDrop(SqueezableInstrument instrument) {
       
	}

}
