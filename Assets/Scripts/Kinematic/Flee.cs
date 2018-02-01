using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {

    public GameObject target;
    private float speed = 0.5f;

    void Update() {
        // If this player is the target (being chased), give it a fleeing velocity
        if(tag == "Target") {
            target = GameObject.FindGameObjectWithTag("Tagged Player");
            GetComponent<Rigidbody>().velocity = -((target.transform.position - transform.position).normalized * speed);
        }
    }
}
