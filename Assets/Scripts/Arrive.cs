using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour {

    public GameObject target;
    float speed = 0.5f;
    float nearSpeed = 0.15f;
    float nearRadius = 1.0f;
    float arrivalRadius = 1.0f;
    float distanceFromTarget;

    void Start() {
        if(tag == "Tagged Player") {
            if (!target) {
                target = GameObject.FindGameObjectWithTag("Target");
            }
        }
    }

    void Update() {
        //calculate the distance from your target and check it against the various radii, have 
        //used the different speed constants to slow down and eventually stop as they are crossed.
        if(tag == "Tagged Player") {
            distanceFromTarget = (target.transform.position - transform.position).magnitude;
            if (distanceFromTarget > nearRadius) {
                Debug.Log("Distance Near Radius " + distanceFromTarget);
                GetComponent<Rigidbody>().velocity = ((target.transform.position - transform.position).normalized * speed);
            }
            else if (distanceFromTarget > arrivalRadius) {
                Debug.Log("Distance Near Radius " + distanceFromTarget);
                GetComponent<Rigidbody>().velocity = ((target.transform.position - transform.position).normalized * nearSpeed);
            }
            else {
                Debug.Log("Inside Arrival Radius " + distanceFromTarget);
                GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        
    }
}
