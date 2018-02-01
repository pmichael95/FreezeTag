using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour {

    public GameObject target;
    public bool hasTarget;
    Quaternion lookWhereYoureGoing;

    Vector3 goalFacing;
    float rotationSpeedRads = 1.0f;

    void Start() {
        if(tag == "Tagged Player") {
            hasTarget = true;
        }
    }

    void Update() {
        target = GetComponent<Seek>().target;
        if (hasTarget) {
            goalFacing = (target.transform.position - transform.position).normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
        else {
            goalFacing = GetComponent<Rigidbody>().velocity.normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
    }
}
