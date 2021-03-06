﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour
{
    #region ABOUT
    /*
     * This script's intended purpose is to provide Steering Arrive behavior to units.
     * We try to arrive to the 'target' given constraints/variables for speed and the likes.
     * Additionally, we also have a Steering Align function here that is called when using Arrive.
     */ 
    #endregion

    #region STEERING ARRIVE VARIABLES
    [Tooltip("The target to arrive to.")]
    public GameObject target;

    // Acquire the above target, if not specified, through the code of Seek
    private SteeringSeek seekTarget;

    private Rigidbody mRigidBody;

    // -- Steering Arrive Variables
    private float maxDistance = 1.0f;
    private float velocityThreshold = 1.0f;
    private float angleThreshold = 3.0f;
    private float maxAcceleration = 3.0f;
    private float slowDownRadius = 2.0f;
    private Vector3 mVelocity;
    private float time_to_target = 0.5f;
    private const float ANGLE_ARC = 45.0f;
    private const float MAX_VELOCITY = 3.0f;

    // -- Steering Align variables

    // The maximum rotation acceleration radians
    private float maxRotationAccelerationRads = 2.0f;
    private float maxAngularVelocity = 180.0f;
    private float maxAngleAcceleration = 150.0f;
    private float slowDownOrientation = 90.0f;
    // By default, it's 0, will change after 1st alignment
    private float angularVel = 0.0f;

    #endregion

    void Start()
    {
        seekTarget = GetComponent<SteeringSeek>();
        mRigidBody = GetComponent<Rigidbody>();
    }

	void Update () {
        // Called only when the mode state is STEERING
        // AND if we're the tagged player OR the number of frozen units except the tagged unit is less than the maximum number of players allowed (other than tagged unit)
        // i.e. if there's a unit to seek/arrive to that is frozen, for example.
        if (GameController.currentState == GameController.ModeState.STEERING && (tag == "Tagged Player" || GameController.numNotFrozenExceptTagged < GameController.MAX_NUM_PLAYERS_EXCEPT_TAGGED)) {
            SteeringArriveBehavior();
        }
    }

    private void SteeringAlignBehavior()
    {
        Vector3 targetTransform = target.transform.position;

        // Acquire the cross product for the sign
        float crossResult = Vector3.Cross(transform.forward, targetTransform).y;

        int sign_bit = 0;
        
        // Set the sign bit
        if (crossResult > 0)
        {
            sign_bit = 1; 
        }
        else
        {
            sign_bit = -1;
        }

        // Find the angle difference to align to
        float differenceAngle = Vector3.Angle(transform.forward, targetTransform);

        if (differenceAngle > maxRotationAccelerationRads)
        {
            float goalVelocity = (maxAngularVelocity * differenceAngle) / slowDownOrientation;
            float goalAcceleration = (goalVelocity - angularVel) / time_to_target;

            if (goalAcceleration > maxAngleAcceleration)
            {
                // Clamp the goal acceleraiton if it surpasses the maximum
                goalAcceleration = maxAngleAcceleration;
            }

            // Multiply by time.deltatime since not in FixedUpdate for consistency
            angularVel += goalAcceleration * Time.deltaTime;
            if (angularVel > maxAngularVelocity)
            {
                // Clamp the angular velocity if it surpasses the maximum
                angularVel = maxAngularVelocity;
            }

            transform.Rotate(transform.up, angularVel * Time.deltaTime * sign_bit, Space.World);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(targetTransform);
        }
    }

    private void SteeringArriveBehavior()
    {
        target = seekTarget.target;
        Vector3 targetTransform = target.transform.position;

        Vector3 direction = targetTransform - transform.position;

        if (direction.magnitude < maxDistance)
        {
            // Step directly to target's position
            transform.position = targetTransform;
            mRigidBody.velocity = Vector3.zero;
        }
        else if(mVelocity.magnitude < velocityThreshold){
            if(direction.magnitude <= maxDistance){
                // Step directly to target's position
                transform.position = targetTransform;
                mRigidBody.velocity = Vector3.zero;
            }
            else{
                SteeringAlignBehavior(); // Align before continuing

                if(Vector3.Angle(transform.forward, direction) <= angleThreshold){
                    Vector3 accel = maxAcceleration * direction.normalized;
                    // Time.deltaTime since not in FixedUpdate
                    mVelocity += accel * Time.deltaTime;

                    if(mVelocity.magnitude > maxAcceleration){
                        // Clamp the velocity so its magnitude is within maximum
                        mVelocity = mVelocity.normalized * maxAcceleration;
                    }

                    transform.Translate(transform.forward * mVelocity.magnitude * Time.deltaTime, Space.World);
                }
            }
        }
        else{
            if (Vector3.Angle(transform.forward, direction) <= ANGLE_ARC)
            {
                Vector3 accelerate;
                if (direction.magnitude < slowDownRadius)
                {
                    float goalFacing = (MAX_VELOCITY * direction.magnitude) / slowDownRadius;
                    float accel = (goalFacing - mVelocity.magnitude) / time_to_target;
                    accelerate = accel * direction.normalized;
                }
                else
                {
                    accelerate = maxAcceleration * direction.normalized;
                }

                // Time.deltaTime since not in FixedUpdate
                mVelocity += accelerate * Time.deltaTime;

                if (mVelocity.magnitude > maxAcceleration)
                {
                    // Clamp velocity's magnitude so it doesn't exceed maximum
                    mVelocity = mVelocity.normalized * maxAcceleration;
                }

                transform.Translate(transform.forward * mVelocity.magnitude * Time.deltaTime, Space.World);
            }

            SteeringAlignBehavior();
        }
    }
}
