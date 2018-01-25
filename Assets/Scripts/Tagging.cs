using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tagging : MonoBehaviour {

    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Tagged Player") {
            if (tag != "Frozen" && tag != "Tagged Player") {
                tag = "Frozen";
                Debug.Log("Froze target");
            }
        }
    }
}
