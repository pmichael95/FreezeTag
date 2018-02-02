using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAlign : MonoBehaviour {

    [Tooltip("The target to align to.")]
    public GameObject target;

    // -- Steering Align variables

    // The maximum rotation acceleration radians
    private float maxRotationAccelerationRads = 2.0f;
    
    private float timeToTarget;
    private float maxAngularVelocity = 180.0f;
    private float maxAngularAccel = 150.0f;
    private float slowDownOrientation = 90.0f;
    // By default, it's 0, will change after 1st alignment
    private float angularVelocity = 0.0f;
    private float time_to_target = 0.5f;

    // The seek object for the target
    private SteeringSeek steerSeek;
    // If we have a target to align to
    private bool hasTarget = false;

    void Start() {
        steerSeek = GetComponent<SteeringSeek>();
    }

    void Update() {
        if(GameController.currentState == GameController.ModeState.STEERING && tag == "Tagged Player") {
            SteeringAlignBehavior();
        }
    }

    private void SteeringAlignBehavior()
    {
        // Acquire the target
        target = steerSeek.target;
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
