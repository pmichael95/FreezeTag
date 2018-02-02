using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour {

    [Tooltip("The target to arrive at.")]
    public GameObject target;

    // -- Arrive Variables
    float speed = 1.0f;
    float nearSpeed = 0.15f;
    float nearRadius = 1.0f;
    float arrivalRadius = 0.5f;
    float distanceFromTarget;

    // The rigidbody of this unit
    private Rigidbody mRigidBody;

    // The seek component for its target
    private Seek seekTarget;

    void Start() {
        mRigidBody = GetComponent<Rigidbody>();
        seekTarget = GetComponent<Seek>();
    }

    void Update() {
        if(GameController.currentState == GameController.ModeState.KINEMATIC && tag == "Tagged Player") {
            ArriveBehavior();
        }
    }

    void ArriveBehavior() {
        // Find the target if it's being seeked
        if (tag == "Tagged Player") {
            target = seekTarget.target;
            distanceFromTarget = (target.transform.position - transform.position).magnitude;
            if (distanceFromTarget > nearRadius) {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * speed);
            }
            else if (distanceFromTarget > arrivalRadius) {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * nearSpeed);
            }
            else {
                mRigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }
    
}
