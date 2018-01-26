using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {
    
    float wanderCircleCenterOffset = 50.0f;
    float wanderCircleRadius = 50.0f;
    float maxWanderVariance = 0.0f;
    float speed = 0.5f;

    void Update() {
        if(tag != "Tagged Player") {
            Vector3 currentRandomPoint = WanderCirclePoint();
            Vector3 moveDirection = (currentRandomPoint - transform.position).normalized;
            GetComponent<Rigidbody>().velocity = (moveDirection * speed);
        }
    }

    Vector3 WanderCirclePoint() {
        Vector3 wanderCircleCenter = transform.position + (Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * wanderCircleCenterOffset);
        Vector3 wanderCirclePoint = wanderCircleRadius * (new Vector3(Mathf.Cos(Random.Range(maxWanderVariance, Mathf.PI - maxWanderVariance)),
                                                            0.0f,
                                                            Mathf.Sin(Random.Range(maxWanderVariance, Mathf.PI - maxWanderVariance))));

        return (wanderCirclePoint + wanderCircleCenter);
    }
}
