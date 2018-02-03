using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {

    [Tooltip("The target to flee from.")]
    public GameObject target;

    // The speed for fleeing
    private float speed = 0.5f;
    private float seekSpeed = 0.5f;

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
        Vector3 fleeDirection = transform.position - target.transform.position;
        if (fleeDirection.magnitude < fleeDistanceThreshold)
        {
            // Step away immediately
            transform.Translate(fleeDirection.normalized * speed * Time.deltaTime, Space.World);
        }
        else
        {
            if (Vector3.Angle(transform.forward, fleeDirection) <= angleThreshold)
            {
                // Rotate and then move away
                transform.Translate(transform.forward * seekSpeed * Time.deltaTime, Space.World);
            }
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(fleeDirection), 180.0f * Time.deltaTime);
    }
}
