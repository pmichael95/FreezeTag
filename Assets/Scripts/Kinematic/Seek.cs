using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{

    #region SEEK VARIABLES
    [Tooltip("The target to seek.")]
    public GameObject target;

    // All possible players (units)
    private GameObject[] players;

    // Seek speed
    float speed = 2.0f;

    // This unit's rigidbody
    private Rigidbody mRigidBody;

    // The game controller 
    private GameController gController;

    #endregion

    void Start() {
        mRigidBody = GetComponent<Rigidbody>();
        gController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    void Update() {
        if(GameController.currentState == GameController.ModeState.KINEMATIC) {
            SeekBehavior();
        }
    }

    private void SeekBehavior() {
        // If you're the tagged player, actively seek a non-frozen character
        if (tag == "Tagged Player") {
            if (!target)
            {
                FindTarget();
            }
            target.tag = "Target";
            mRigidBody.velocity = ((target.transform.position - transform.position).normalized * speed);
        }
        // If you're not the tagged player, try and seek a frozen player to unfreeze
        else if (tag == "Not Frozen")
        {
            FindFrozenTarget();
            if (target)
            {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * speed);
            }
        }
    }

    private void FindTarget() {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;
       
        foreach (GameObject player in gController.GetPlayers()) {
            if(player != this.gameObject && player.gameObject.tag != "Frozen") {
                if(OldDistanceToPlayer != Vector3.zero) {
                    distanceToPlayer = (player.transform.position - transform.position).normalized;
                    if(distanceToPlayer.magnitude < OldDistanceToPlayer.magnitude) {
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
