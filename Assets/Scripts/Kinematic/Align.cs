using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour {

    [Tooltip("The target to align to.")]
    public GameObject target;

    [Tooltip("True if we are aligning to a target.")]
    public bool hasTarget;

    // Align variables, where we're looking at, the facing goal, and the rotation's speed
    Quaternion lookWhereYoureGoing;
    Vector3 goalFacing;
    float rotationSpeedRads = 1.5f;

    // This unit's rigidbody
    private Rigidbody mRigidBody;
    // The seek'd target to align to
    private Seek seekTarget;

    void Start() {
        /*
        if(tag == "Tagged Player") {
            hasTarget = true;
        }
        */
        mRigidBody = GetComponent<Rigidbody>();
        seekTarget = GetComponent<Seek>();
    }

    void Update() {
        if(GameController.currentState == GameController.ModeState.KINEMATIC) {
            AlignBehavior();
        }
    }
    
    void AlignBehavior() {
        target = seekTarget.target;

        if (target) {
            hasTarget = true;
        }
        else {
            hasTarget = false;
        }

        if (hasTarget) {
            goalFacing = (target.transform.position - transform.position).normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
        else {
            goalFacing = mRigidBody.velocity.normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
    }
}
