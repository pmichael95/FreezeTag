using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour {

    public GameObject target;
    float speed = 0.5f;

    void Start() {
        if(tag == "Tagged Player") {
            if (!target) {
                target = GameObject.FindGameObjectWithTag("Target");
            }
        }
    }

    void Update() {
        //Seek should move TOWARD the target object.
        if(target)
            GetComponent<Rigidbody>().velocity = ((target.transform.position - transform.position).normalized * speed);
    }
}
