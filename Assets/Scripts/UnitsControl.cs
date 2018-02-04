using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsControl : MonoBehaviour
{

    #region ABOUT
    /*
     * This script's intended purpose is to freeze a unit when its tag changes.
     * This is not dependent on the game state.
     */
    #endregion

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
