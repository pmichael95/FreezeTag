using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringFlee : MonoBehaviour
{

    #region STEERING FLEE VARIABLES
    [Tooltip("The tagged player to flee from.")]
    public GameObject target;

    private float maxAccel = 1.0f;
    private Vector3 mVelocity;
    private float fleeDistanceThreshold = 2.0f;
    private Rigidbody mRigidBody;

    #endregion

    #region STEERING ALIGN VARIABLES
    // The maximum rotation acceleration radians
    private float maxRotationAccelerationRads = 2.0f;
    private float maxAngularVelocity = 180.0f;
    private float maxAngularAccel = 150.0f;
    private float slowDownOrientation = 90.0f;
    // By default, it's 0, will change after 1st alignment
    private float angularVelocity = 0.0f;
    private float time_to_target = 0.5f;

    #endregion

    void Start () {
        mRigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        if (GameController.currentState == GameController.ModeState.STEERING && tag == "Target") {
            SteeringFleeBehavior();
        }
    }

    private void SteeringFleeBehavior()
    {
        target = GameObject.FindGameObjectWithTag("Tagged Player");
        Vector3 fleeDirection = transform.position - target.transform.position;
        SteeringAlignBehavior();

        Vector3 acceleration = maxAccel * fleeDirection.normalized;

        // Time.deltaTime since not in FixedUpdate
        mVelocity += acceleration * Time.deltaTime;

        if (mVelocity.magnitude > maxAccel)
        {
            mVelocity = mVelocity.normalized * maxAccel;
        }

        if (fleeDirection.magnitude < fleeDistanceThreshold)
        {
            // Step Away even if sidestepping
            transform.Translate(mVelocity * Time.deltaTime, Space.World);
        }
        else
        {
            // Face AWay delegate
            transform.Translate(transform.forward * mVelocity.magnitude * Time.deltaTime, Space.World);
        }
    }

    private void SteeringAlignBehavior()
    {
        Vector3 targetTransform = target.transform.position;

        int signbit = Vector3.Cross(transform.forward, targetTransform).y > 0 ? 1 : -1;

        // Find the angle difference to align to
        float differenceAngle = Vector3.Angle(transform.forward, targetTransform);

        if (differenceAngle > maxRotationAccelerationRads)
        {
            float goalVelocity = (maxAngularVelocity * differenceAngle) / slowDownOrientation;
            float goalAcceleration = (goalVelocity - angularVelocity) / time_to_target;

            if (goalAcceleration > maxAngularAccel)
            {
                // Clamp the goal acceleraiton if it surpasses the maximum
                goalAcceleration = maxAngularAccel;
            }

            // Multiply by time.deltatime since not in FixedUpdate for consistency
            angularVelocity += goalAcceleration * Time.deltaTime;
            if (angularVelocity > maxAngularVelocity)
            {
                // Clamp the angular velocity if it surpasses the maximum
                angularVelocity = maxAngularVelocity;
            }

            transform.Rotate(transform.up, angularVelocity * Time.deltaTime * signbit, Space.World);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(targetTransform);
        }
    }
}
