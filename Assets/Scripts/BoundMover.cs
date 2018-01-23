using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundMover : MonoBehaviour {

    // When any unit collides with the bounding walls, we move them to the opposite end appropriately
    // Assumption: Only units can possibly collide with the bounds (they are the only possible collidable objects)
    void OnTriggerEnter(Collider col) {
        if(this.gameObject.tag == "Top & Bottom Bounds") {
            float newZ = -col.gameObject.transform.position.z;
            col.gameObject.transform.position = new Vector3(col.gameObject.transform.position.x, col.gameObject.transform.position.y, newZ);
        }
        else if(this.gameObject.tag == "Right & Left Bounds") {
            float newX = -col.gameObject.transform.position.x;
            col.gameObject.transform.position = new Vector3(newX, col.gameObject.transform.position.y, col.gameObject.transform.position.z);
        }
    }
}
