using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {

    #region ABOUT
    /*
     * This script's intended purpose is to provide Kinematic Flee behavior to units.
     * If this unit is the targetted player, he will align in the opposite direction of the tagged player, and flee.
     * Alignment is done thanks to transform.rotation
     */
    #endregion

    [Tooltip("The target to flee from.")]
    public GameObject target;

    // The speed for fleeing
    private float speed = 0.5f;

    private float fleeDistanceThreshold = 1.0f;

    private float angleThreshold = 1.0f;

    // This unit's rigidbody
    private Rigidbody mRigidBody;

    void Start() {
        mRigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        if(GameController.currentState == GameController.ModeState.KINEMATIC && tag == "Target") {
            // FleeBehavior();
            KinematicFlee();
        }
    }

    private void FleeBehavior() {
        // If this player is the target (being chased), give it a fleeing velocity
        if(tag == "Target") {
            target = GameObject.FindGameObjectWithTag("Tagged Player");
            mRigidBody.velocity = -((target.transform.position - transform.position).normalized * speed);
        }
    }

    private void KinematicFlee()
    {
        target = GameObject.FindGameObjectWithTag("Tagged Player");
        Vector3 fleeDir = transform.position - target.transform.position;
        if (fleeDir.magnitude < fleeDistanceThreshold)
        {
            // Step away immediately
            transform.Translate(fleeDir.normalized * speed * Time.deltaTime, Space.World);
        }
        else
        {
            if (Vector3.Angle(transform.forward, fleeDir) <= angleThreshold)
            {
                transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            }
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(fleeDir), 180.0f * Time.deltaTime);
    }
}
