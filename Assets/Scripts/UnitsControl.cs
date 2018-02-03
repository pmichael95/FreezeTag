using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsControl : MonoBehaviour {

    /*
     * This script's intended purpose is to initiate an effect to tell that a unit is frozen.
     * Additionally, it handles unfreezing, if the unit originally is not frozen.
     */
    #region UNITS CONTROL VARIABLES
    private Rigidbody mRigidBody;

    #endregion

    // Use this for initialization
	void Start () {
        mRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (tag == "Frozen")
        {
            mRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
	}
}
