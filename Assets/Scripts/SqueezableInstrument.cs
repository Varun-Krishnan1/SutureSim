using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR; 
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Interactable))]
public class SqueezableInstrument : MonoBehaviour
{
    [Header("Instrument Parts")]
    public GameObject instrumentPart1; 
    public GameObject instrumentPart2; 
    public Transform pivot1; // -- where to rotate part1 from (if single pivot object such as scissor set pivot1 and pivot2 as same transform!)
    public Transform pivot2; // -- where to rotate part2 from 
    public SqueezableTip squeezableTip; // -- game object on tip of instrument that detects if needle collider is touching

    [Header("Instrument Paramaters")]
    public float pivotScale; // -- how much to rotate the parts of the instrument 
    [Range(0.0f,1.0f)] public float closedCutoff; // -- at what point of previousAxis to consider instrument closed 

    [Header("Logging")]
    public bool logging; // -- for popup text on top of instrument or in console 
    public Text needlePopup; // -- where debug text is shown in game  


    // -- PRIVATE VARIABLES -- 
    private Interactable interactable;
    private SteamVR_Action_Single gripSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");
    private new Rigidbody rigidbody;
    
    private bool closed; 
    private Needle curNeedle; // -- stores needle if a needle is in collision box of squeezableTip 

    private float previousAxis = 0; // -- previous frame's grip squeeze amount
    private Vector3 rotateAmount1, rotateAmount2;  // -- internal calculation of how much to rotate each part determined by previous axis and rotate scale

    private bool justClosed, justOpened; // -- used for "cayote opening" so leeway in opening and closing with needle being detected 
    private float justClosedTimer = 1f; // -- used for "cayote opening" "
    private float justClosedCurTimer; // -- used for "cayote opening"

    private bool positionFrozen; 
    public Vector3 frozenPosition; 
    public Vector3 frozenRotation; 

    public bool frozenX;
    public bool frozenY; 
    public bool frozenZ; 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        interactable = GetComponent<Interactable>();

        if(!logging) {
            needlePopup.transform.parent.gameObject.SetActive(false);  // -- hide entire canvas 
		}
    }

    // Update is called once per frame
    void Update()
    {
        // -- Debugging 
        if(squeezableTip.needle) {
            if(curNeedle) {
                DebugLog("Needle attached", true, false);
			}
            else {
                DebugLog("Needle grabbable", true, false);  
			}
		}
        else {
            DebugLog("No Needle", true, false);
		}

        float grip = 0;

        if (interactable.attachedToHand)
        {
            grip = gripSqueeze.GetAxis(interactable.attachedToHand.handType);
        }


        if (grip != previousAxis)
        {

            if (grip > closedCutoff)
            {
                closed = true;
            }
            else
            {
                closed = false;
            }

            instrumentPart1.transform.RotateAround(pivot1.position, instrumentPart1.transform.forward * -1, (pivotScale * previousAxis));
            instrumentPart2.transform.RotateAround(pivot2.position, instrumentPart2.transform.forward, (pivotScale * previousAxis));

            rotateAmount1 = instrumentPart1.transform.forward;
            rotateAmount2 = instrumentPart2.transform.forward * -1;

            instrumentPart1.transform.RotateAround(pivot1.position, rotateAmount1, pivotScale * grip);
            instrumentPart2.transform.RotateAround(pivot2.position, rotateAmount2, pivotScale * grip);

            // -- Just closed
            if (closed && previousAxis < closedCutoff)
            {
                justClosedCurTimer = justClosedTimer;
            }
            // -- Just released 
            else if (!closed && previousAxis > closedCutoff)
            {
                justClosedCurTimer = 0f;
                ReleaseNeedle();
            }
            previousAxis = grip;
        }

        if (justClosedCurTimer > 0)
        {
            Needle needle = squeezableTip.needle;

            if (needle)
            {
                AttachNeedle(needle);
            }

            justClosedCurTimer -= Time.deltaTime;
        }

    }

    void DebugLog(string text, bool onPopup, bool inConsole) {
        if(logging) {
            if(onPopup) {
                needlePopup.text = text;
		    }
            if(inConsole) {
                Debug.Log(text);   
		    }
		}
	}

    void AttachNeedle(Needle needle) {
        if(!curNeedle) {
            DebugLog("Attaching!", false, true);
            needle.stateMachine.currentState.InstrumentGrab(this); // -- let FSM decide what to do 

            curNeedle = needle; 
		}

	}

    void ReleaseNeedle() {
        if(curNeedle) {
            DebugLog("Releasing", false, true);
            curNeedle.stateMachine.currentState.InstrumentDrop(this); // -- let FSM decide what to do 

            curNeedle = null; 

		}

	}

    public bool isClosed() {
        return closed; 
	}

        
    void LateUpdate() {
        if(positionFrozen) {
            transform.position = new Vector3(frozenPosition.x, transform.position.y, transform.position.z);

            float x = transform.rotation.eulerAngles.x; 
            float y = transform.rotation.eulerAngles.y; 
            float z = transform.rotation.eulerAngles.z; 
            if(frozenX) {
                x = frozenRotation.x;      
			}
            if(frozenY) {
                y = frozenRotation.y;      
			}
            if(frozenZ) {
                z = frozenRotation.z;      
			}
            transform.eulerAngles = new Vector3(x, y, z); 
		}
	}

    public void FreezePosition() {
        if(!positionFrozen) {
            frozenPosition = transform.position;
            frozenRotation = transform.rotation.eulerAngles; 
            positionFrozen = true; 
		}

	}

    public void UnfreezePosition() {
        if(positionFrozen) positionFrozen = false; 
	}
}
