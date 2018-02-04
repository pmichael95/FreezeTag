using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{

    #region ABOUT
    /*
     * This script's intended purpose is to provide Kinematic Arrive behavior to units.
     * We try to arrive (after aligning) to a target if we're the tagged player.
     * The target is initially the closest unit at a given time.
     */ 
    #endregion

    #region ARRIVE VARIABLES
    [Tooltip("The target to arrive at.")]
    public GameObject target;

    [Tooltip("True if we are aligning to a target.")]
    public bool hasTarget;

    // Align variables, where we're looking at, the facing goal, and the rotation's speed
    Quaternion lookWhereYoureGoing;
    Vector3 goalFacing;
    float rotationSpeedRads = 1.5f;

    // -- Arrive Variables
    float speed = 1.0f;
    float nearSpeed = 1.0f;
    float nearRadius = 1.0f;
    float arrivalRadius = 2.0f;
    float distanceFromTarget;

    // The rigidbody of this unit
    private Rigidbody mRigidBody;

    // The seek component for its target
    private Seek seekTarget;

    #endregion

    void Start() {
        mRigidBody = GetComponent<Rigidbody>();
        seekTarget = GetComponent<Seek>();
    }

    void Update() {
        if (GameController.currentState == GameController.ModeState.KINEMATIC)
        {
            AlignBehavior();
            ArriveBehavior();
        }
    }

    private void ArriveBehavior() {
        // Find the target if it's being seeked
        if (tag == "Tagged Player") {
            target = seekTarget.target;
            distanceFromTarget = (target.transform.position - transform.position).magnitude;
            if (distanceFromTarget > nearRadius) {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * speed);
            }
            else if(distanceFromTarget <= arrivalRadius){
                // Step directly
                mRigidBody.velocity = Vector3.zero;
                transform.position = target.transform.position;
            }
            else if (distanceFromTarget > arrivalRadius) {
                // If we're still greater than the arrival radius, continue with velocity
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * nearSpeed);
            }
            else {
                // Otherwise, end the velocity, we're in
                mRigidBody.velocity = Vector3.zero;
            }
        }
    }

    private void AlignBehavior()
    {
        target = seekTarget.target;

        if (target)
        {
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }

        // If there's a target, we use it to set our facing goal
        if (hasTarget)
        {
            goalFacing = (target.transform.position - transform.position).normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
        else
        {
            // Otherwise, simply use velocity's normalized vector as the goal (face where we're moving)
            goalFacing = mRigidBody.velocity.normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
    }
    
}
