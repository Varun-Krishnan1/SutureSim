using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleCollider : MonoBehaviour
{
    public Needle needle; 

    public enum LocationOnNeedle {Tip, Bottom, Middle, Middle_top, Top}; 
    
    public LocationOnNeedle locationOnNeedle; 

    public Needle GetNeedle() {
        return needle; 
	}

    public bool isNeedleTip() {
        return locationOnNeedle == LocationOnNeedle.Tip; 
	}
}
