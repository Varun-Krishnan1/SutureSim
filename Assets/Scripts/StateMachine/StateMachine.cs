using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public StateInterface currentState;

    public void Initialize(StateInterface state)
    {
        currentState = state; 
        currentState.Enter(); 
    }

    // Update is called once per frame
    public void Update()
    {
        if(currentState != null) {
            currentState.Execute();   
		}
    }

    public void ChangeState(StateInterface newState) {
        if(currentState != null) {
            currentState.Exit();   
		}
            
        currentState = newState; 
        currentState.Enter(); 
	}
}
