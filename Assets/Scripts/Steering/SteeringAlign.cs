using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAlign : MonoBehaviour {

    [Tooltip("The target to align to.")]
    public GameObject target;

    // -- Steering Align variables
    // The quaternion for where we're rotating to
    private Quaternion lookWhereYoureGoing;
    // The goal to face to
    private Vector3 goalFacing;
    private float orientationRads;
    private float rotationSpeedRads;
    // The threshold for slowing down to align
    private float slowDownThreshold = 1.0f;
    private float goalRotationSpeedRads;
    // The maximum rotation speed radians
    private float maxRotationSpeedRads = 1.0f;
    // The maximum rotation acceleration radians
    private float maxRotationAccelerationRads = 2.0f;
    // Acceleration radians for speed adjustments
    private float accelerationRads = 1.0f;
    private float timeToTarget;

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

    private void SteeringAlignBehavior() {
        // Acquire the target
        target = steerSeek.target;

        goalFacing = (target.transform.position - transform.position).normalized;

        // Generate the desired speed to reach
        goalRotationSpeedRads = maxRotationSpeedRads * (Vector3.Angle(goalFacing, this.transform.forward) / slowDownThreshold);
        Debug.Log("goalRotationSpeedRads: " + goalRotationSpeedRads);
        // Calculate the time to target based on the rotation speeds
        if (rotationSpeedRads != 0.0f) {
            timeToTarget = Vector3.Angle(goalFacing, this.transform.forward) / rotationSpeedRads;
        }
        else {
            timeToTarget = 1.0f;
        }

        // Compute the acceleration
        accelerationRads = (goalRotationSpeedRads - rotationSpeedRads) / timeToTarget;
        Debug.Log("AccelerationRads: " + accelerationRads);

        // Enforce the max rotational acceleration
        if (accelerationRads >= maxRotationAccelerationRads) {
            accelerationRads = maxRotationAccelerationRads;
        }
        Debug.Log("AccelerationRads after check: " + accelerationRads);
        // Apply the acceleration to the current rotation speed
        rotationSpeedRads = rotationSpeedRads + (accelerationRads * Time.deltaTime);
        Debug.Log("Rotation speed rads: " + rotationSpeedRads);
        if (rotationSpeedRads >= maxRotationSpeedRads) {
            rotationSpeedRads = maxRotationSpeedRads;
        }
        Debug.Log("Rotation speed rads after check: " + rotationSpeedRads);
        // Quaternion.LookRotation() to generate the quaternion for the orientation
        lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
        // Then use rotate towards for the rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        Debug.Log("transform rotation: " + transform.rotation);
    }
}
