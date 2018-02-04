using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringFlee : MonoBehaviour
{
    #region ABOUT
    /*
     * This script's intended purpose is provide Steering Flee behavior to units being pursued.
     * If a unit is being chased/pursued by the tagged player, it will align in the opposite direction, and flee.
     * Alignment is handled thanks to transform.rotation
     */
    #endregion

    #region STEERING FLEE VARIABLES
    [Tooltip("The tagged player to flee from.")]
    public GameObject target;

    private float maxAccel = 1.0f;
    private Vector3 mVelocity;
    private float fleeDist = 2.0f;
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
        Vector3 fleeDir = transform.position - target.transform.position;

        Vector3 acceleration = maxAccel * fleeDir.normalized;

        // Time.deltaTime since not in FixedUpdate
        mVelocity += acceleration * Time.deltaTime;

        if (mVelocity.magnitude > maxAccel)
        {
            mVelocity = mVelocity.normalized * maxAccel;
        }

        if (fleeDir.magnitude < fleeDist)
        {
            // Step Away even if sidestepping
            transform.Translate(mVelocity * Time.deltaTime, Space.World);
        }
        else
        {
            // Face AWay delegate
            transform.Translate(transform.forward * mVelocity.magnitude * Time.deltaTime, Space.World);
        }

        // Rotate to face the opposite direction of the tagged player chasing you
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(fleeDir), 180.0f * Time.deltaTime);
    }
}
