using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Needle needleObject = other.GetComponent<Needle>();
        if(needleObject) {
            needle = needleObject;    
		}
	}

    void OnTriggerExit(Collider other) {
        Needle needleObject = other.GetComponent<Needle>();
        if(needleObject) {
            needle = null;    
		}
	}
}
