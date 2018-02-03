using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{

    #region WANDER VARIABLES
    float wanderCircleCenterOffset = 50.0f;
    float wanderCircleRadius = 5.0f;
    float maxWanderVariance = 0.0f;
    float speed = 0.5f;
    private Rigidbody mRigidBody;
    #endregion

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        // If we're not the tagged player, allow to wander
        if(tag != "Tagged Player") {
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
