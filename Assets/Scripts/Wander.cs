using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    #region ABOUT
    /*
     * This script's intended purpose is to add a 'Wander' movement AI to all units.
     * In fact, as long as a unit is not frozen and is not the tagged player, they will wander
     * to random points in the play area, distinctly.
     */ 
    #endregion

    #region WANDER VARIABLES
    float wanderCircleCenterOffset = 50.0f;
    float wanderCircleRadius = 5.0f;
    float maxWanderVariance = 10.0f;
    float speed = 0.5f;
    private Rigidbody mRigidBody;
    #endregion

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        // If we're not the tagged player, allow to wander
        if(tag != "Tagged Player" && tag != "Frozen") {
            Vector3 currRandPt = WanderCirclePoint();
            Vector3 moveDir = (currRandPt - transform.position).normalized;
            mRigidBody.velocity = (moveDir * speed);
        }
    }

    // Acquires a random wander circle point
    Vector3 WanderCirclePoint() {
        Vector3 wanderCircleCenter = transform.position + (Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * wanderCircleCenterOffset);
        Vector3 wanderCirclePoint = wanderCircleRadius * (new Vector3(Mathf.Cos(Random.Range(maxWanderVariance, Mathf.PI - maxWanderVariance)),
                                                            0.0f,
                                                            Mathf.Sin(Random.Range(maxWanderVariance, Mathf.PI - maxWanderVariance))));

        return (wanderCirclePoint + wanderCircleCenter);
    }
}
