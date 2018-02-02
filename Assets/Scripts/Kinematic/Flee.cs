using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {

    [Tooltip("The target to flee from.")]
    public GameObject target;

    // The speed for fleeing
    private float speed = 0.5f;

    // This unit's rigidbody
    private Rigidbody mRigidBody;

    void Start() {
        mRigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        if(GameController.currentState == GameController.ModeState.KINEMATIC) {
            FleeBehavior();
        }
    }

    private void FleeBehavior() {
        // If this player is the target (being chased), give it a fleeing velocity
        if(tag == "Target") {
            target = GameObject.FindGameObjectWithTag("Tagged Player");
            mRigidBody.velocity = -((target.transform.position - transform.position).normalized * speed);
        }
    }
}
