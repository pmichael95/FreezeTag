using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeek : MonoBehaviour {

    [Tooltip("The target to seek to.")]
    public GameObject target;

    // -- Steering seek variables
    float acceleration = 1.5f;
    float maxSpeed = 5.0f;
    float distanceFromTarget;

    // This unit's rigidbody
    private Rigidbody mRigidBody;

    private GameController gController;

    // Use this for initialization
    void Start () {
        mRigidBody = GetComponent<Rigidbody>();
        gController = GameObject.Find("Game Controller").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.currentState == GameController.ModeState.STEERING) {
            SteeringSeekBehavior();
        }
    }

    private void FindTarget() {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;

        foreach (GameObject player in gController.GetPlayers()) {
            if (player != this.gameObject) {
                if (OldDistanceToPlayer != Vector3.zero) {
                    distanceToPlayer = (player.transform.position - transform.position).normalized;
                    if (distanceToPlayer.magnitude < OldDistanceToPlayer.magnitude) {
                        // Set the new target to the newly tested distance
                        target = player;
                    }
                }
                else {
                    OldDistanceToPlayer = (player.transform.position - transform.position).normalized;
                    // By default set it to the first one visited
                    target = player;
                }
            }
        }
    }

    private void SteeringSeekBehavior() {
        if(tag == "Tagged Player") {
            FindTarget();

            if (mRigidBody.velocity.magnitude < maxSpeed) {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * acceleration);
            }
            else {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * maxSpeed);
            }
        }
    }
}
