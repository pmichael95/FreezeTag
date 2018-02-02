using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringFlee : MonoBehaviour {

    [Tooltip("The tagged player to flee from.")]
    public GameObject target;

    float acceleration = 0.5f;
    float maxSpeed = 2.0f;
    float distanceFromTarget;

    private Rigidbody mRigidBody;

    // Use this for initialization
    void Start () {
        mRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.currentState == GameController.ModeState.STEERING && tag == "Target") {
            SteeringFleeBehavior();
        }
    }

    private void SteeringFleeBehavior() {
        target = GameObject.FindGameObjectWithTag("Tagged Player");

        if (mRigidBody.velocity.magnitude < maxSpeed) {
            mRigidBody.velocity = -((target.transform.position - transform.position).normalized * acceleration);
        }
        else {
            mRigidBody.velocity = -((target.transform.position - transform.position).normalized * maxSpeed);
        }
    }
}
