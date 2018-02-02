using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringArrive : MonoBehaviour {

    [Tooltip("The target to arrive to.")]
    public GameObject target;

    // Acquire the above target, if not specified, through the code of Seek
    private SteeringSeek seekTarget;

    private Rigidbody mRigidBody;

    // -- Steering Arrive Variables
    private float distanceThreshold = 1.0f;
    private float velocityThreshold = 1.0f;
    private float angleThreshold = 3.0f;
    private float maxAcceleration = 3.0f;
    private float slowDownRadius = 2.0f;
    private Vector3 mVelocity;
    private float time_to_target = 0.5f;
    private const float ANGLE_ARC = 45.0f;
    private const float MAX_VELOCITY = 3.0f;

    void Start()
    {
        seekTarget = GetComponent<SteeringSeek>();
        mRigidBody = GetComponent<Rigidbody>();
    }

	void Update () {
        if (GameController.currentState == GameController.ModeState.STEERING && tag == "Tagged Player") {
            SteeringArriveBehavior();
        }
    }

    private void SteeringArriveBehavior()
    {
        target = seekTarget.target;
        Vector3 targetTransform = target.transform.position;

        Vector3 direction = targetTransform - transform.position;

        if (direction.magnitude < distanceThreshold)
        {
            // Step directly to target's position
            transform.position = targetTransform;
            mRigidBody.velocity = Vector3.zero;
        }
        else if(mVelocity.magnitude < velocityThreshold){
            if(direction.magnitude <= distanceThreshold){
                // Step directly to target's position
                transform.position = targetTransform;
                mRigidBody.velocity = Vector3.zero;
            }
            else{
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
        }
    }
}
