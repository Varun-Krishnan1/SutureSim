using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SqueezableTip : MonoBehaviour
{

   public Needle needle; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        NeedleCollider needleCollider = other.GetComponent<NeedleCollider>();
        if(needleCollider) {
            needle = needleCollider.GetNeedle();;    
		}
	}

    void OnTriggerExit(Collider other) {
        NeedleCollider needleCollider = other.GetComponent<NeedleCollider>();
        if(needleCollider) {
            needle = null; 
		}
	}
}
