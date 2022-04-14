using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{

    public bool attached; 
    public bool positionFrozen; 
    public Vector3 frozenPosition; 
    public Vector3 frozenRotation; 

    public SqueezableInstrument forcep; 

    public Rigidbody rb; 

    public StateMachine stateMachine; 
    public StateInterface unattachedState;
    public StateInterface attachedState;
    public StateInterface woundState; 

    public SqueezableInstrument instrument; 
    public Wound wound; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 

        stateMachine = new StateMachine(); 
        unattachedState = new NeedleUnattachedState(this); 
        attachedState = new NeedleAttachedState(this); 
        woundState = new NeedleWoundState(this); 

        stateMachine.ChangeState(unattachedState);
    }

    public void ChangeState(StateInterface state) {
        stateMachine.ChangeState(state);
	}

    void Update() {
        stateMachine.currentState.Execute(); 

	}

    
    void LateUpdate() {
        if(positionFrozen) {
            transform.position = frozenPosition;
            // transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, frozenRotation.y, frozenRotation.z);
		}
	}

    public void FreezeInWound() {
        frozenPosition = transform.position;
        frozenRotation = transform.rotation.eulerAngles; 
        positionFrozen = true; 
	}
    
}
