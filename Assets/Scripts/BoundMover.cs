using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundMover : MonoBehaviour {

    // Offset for the position when moving units
    const float POS_OFFSET = 0.5f;

    // When any unit collides with the bounding walls, we move them to the opposite end appropriately
    // Assumption: Only units can possibly collide with the bounds (they are the only possible collidable objects)
    void OnTriggerEnter(Collider col) {
        if(this.gameObject.tag == "Top Bound") {
            // If we collide with the top bound, negate the z-pos and offset by adding a positive value
            Vector3 oldPosition = col.gameObject.transform.position;
            float newZ = - oldPosition.z;
            col.gameObject.transform.position = new Vector3(oldPosition.x, oldPosition.y, newZ + POS_OFFSET);
        }
        else if(this.gameObject.tag == "Bottom Bound") {
            // If we collide with bottom bound, negate the z-pos and subtrat by a positive offset
            Vector3 oldPosition = col.gameObject.transform.position;
            float newZ = -oldPosition.z;
            col.gameObject.transform.position = new Vector3(oldPosition.x, oldPosition.y, newZ - POS_OFFSET);
        }
        else if(this.gameObject.tag == "Right Bound") {
            // If we collide with left bound, negate x-pos, and add by a positive offset
            Vector3 oldPosition = col.gameObject.transform.position;
            float newX = -oldPosition.x;
            col.gameObject.transform.position = new Vector3(newX + POS_OFFSET, oldPosition.y, oldPosition.z);
        }
        else {
            // If we collide with left bound, negate x-pos, and subtract by a positive offset
            Vector3 oldPosition = col.gameObject.transform.position;
            float newX = -oldPosition.x;
            col.gameObject.transform.position = new Vector3(newX - POS_OFFSET, oldPosition.y, oldPosition.z);
        }
    }
}
