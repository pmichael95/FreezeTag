using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundMover : MonoBehaviour {

    const float positionOffset = 0.5f;

    // When any unit collides with the bounding walls, we move them to the opposite end appropriately
    // Assumption: Only units can possibly collide with the bounds (they are the only possible collidable objects)
    void OnTriggerEnter(Collider col) {
        if(this.gameObject.tag == "Top & Bottom Bounds") {
            Vector3 oldPos = col.gameObject.transform.position;
            float newZ = -col.gameObject.transform.position.z;
            col.gameObject.transform.position = new Vector3(oldPos.x, oldPos.y, newZ + positionOffset);
        }
        else if(this.gameObject.tag == "Right & Left Bounds") {
            Vector3 oldPos = col.gameObject.transform.position;
            float newX = -col.gameObject.transform.position.x;
            col.gameObject.transform.position = new Vector3(newX - positionOffset, oldPos.y, oldPos.z);
        }
    }
}
