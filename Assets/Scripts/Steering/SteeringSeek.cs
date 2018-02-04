using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSeek : MonoBehaviour
{
    #region ABOUT
    /*
     * This script's intended purpose is to provide Steering Seek behavior to units.
     * The tagged played will seek a Not Frozen unit that is closest to him at a given time.
     * Additionally, the not frozen units will seek frozen units to unfreeze, that are closer to them at a given time.
     */
    #endregion

    #region STEERING SEEK VARIABLES
    [Tooltip("The target to seek to.")]
    public GameObject target;

    // -- Steering seek variables
    float acceleration = 2.0f;
    float maxSpeed = 15.0f;
    float distanceFromTarget;

    // This unit's rigidbody
    private Rigidbody mRigidBody;

    private GameController gController;

    #endregion

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

    private void SteeringSeekBehavior()
    {
        // If you're the tagged player, actively seek a non-frozen target
        if (tag == "Tagged Player")
        {
            if (! target)
            {
                FindTarget();
            }

            target.tag = "Target";

            if (mRigidBody.velocity.magnitude < maxSpeed)
            {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * acceleration);
            }
            else
            {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * maxSpeed);
            }
        }
        // If not frozen, actively try to unfreeze characters
        else if (tag == "Not Frozen")
        {
            FindFrozenTarget();

            if (mRigidBody.velocity.magnitude < maxSpeed && target)
            {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * acceleration);
            }
            else if(target)
            {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * maxSpeed);
            }
        }
    }

    // Finds a non-frozen character, as the tagged player, to target and seek
    private void FindTarget() {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;

        foreach (GameObject player in gController.GetPlayers()) {
            if (player != this.gameObject && player.gameObject.tag != "Frozen") {
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

    // Finds a frozen character to target and seek
    private void FindFrozenTarget()
    {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;

        foreach (GameObject player in gController.GetPlayers())
        {
            if (player != this.gameObject && player.gameObject.tag == "Frozen")
            {
                if (OldDistanceToPlayer != Vector3.zero)
                {
                    distanceToPlayer = (player.transform.position - transform.position).normalized;
                    if (distanceToPlayer.magnitude < OldDistanceToPlayer.magnitude)
                    {
                        // Set the new target to the newly tested distance
                        target = player;
                    }
                }
                else
                {
                    OldDistanceToPlayer = (player.transform.position - transform.position).normalized;
                    // By default set it to the first one visited
                    target = player;
                }
            }
        }
    }
}
