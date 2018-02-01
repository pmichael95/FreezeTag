using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour {

    public GameObject target;
    float speed = 1.0f;
    float nearSpeed = 0.25f;
    float nearRadius = 1.0f;
    float arrivalRadius = 1.0f;
    float distanceFromTarget;

    void Update() {
        // Find the target if it's being seeked
        if(tag == "Tagged Player") {
            target = GetComponent<Seek>().target;
            distanceFromTarget = (target.transform.position - transform.position).magnitude;
            if (distanceFromTarget > nearRadius) {
                GetComponent<Rigidbody>().velocity = ((target.transform.position - transform.position).normalized * speed);
            }
            else if (distanceFromTarget > arrivalRadius) {
                GetComponent<Rigidbody>().velocity = ((target.transform.position - transform.position).normalized * nearSpeed);
            }
            else {
                GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        
    }
}
