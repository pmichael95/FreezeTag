using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour {

    public GameObject target;
    public bool hasTarget;
    Quaternion lookWhereYoureGoing;

    Vector3 goalFacing;
    float orientationRads;
    float rotationSpeedRads = 2.0f;

    void Start() {
        if(tag == "Tagged Player") {
            hasTarget = true;
        }
    }

    void Update() {
        if (hasTarget) {
            //Figure out where you want to face, then use the mentioned functions to make it happen
            goalFacing = (target.transform.position - transform.position).normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
        else {
            //When the unit has no target, just have its goal facing be it's transform.forward direction.
            goalFacing = GetComponent<Rigidbody>().velocity.normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
    }
}
