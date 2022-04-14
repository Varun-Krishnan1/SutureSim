using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StateInterface {
	public void Enter(); 
	public void Execute(); 
	public void Exit(); 
	public void InstrumentGrab(SqueezableInstrument instrument); 
	public void InstrumentDrop(SqueezableInstrument instrument); 
}

