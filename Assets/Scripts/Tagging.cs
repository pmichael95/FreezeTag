using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tagging : MonoBehaviour
{

    #region ABOUT
    /*
     * This script's intended purpose is to handle tagging/untagging of units based on cases.
     * The first case is when a unit that is NOT the tagged player and is not frozen, collides with the tagged player.
     * At that point, this unit is frozen.
     * Similarly, a frozen unit colliding with a non-frozen unit will be unfrozen.
     * (Additionally, this script uses GameController variables for target resetting and the likes.)
     */
    #endregion

    void OnTriggerEnter(Collider col) {
        // If this unit collides with the tagged player...
        if(col.gameObject.tag == "Tagged Player") {
            // And this unit is not already frozen or the tagged player (error catching)
            if (tag != "Frozen" && tag != "Tagged Player") {
                // Set its tag to be frozen and freeze it.
                tag = "Frozen";
                if (GameController.currentState == GameController.ModeState.KINEMATIC)
                {
                    col.GetComponent<Seek>().target = null;
                }
                else
                {
                    col.GetComponent<SteeringSeek>().target = null;
                }
                // Game Controller settings changes
                GameController.numNotFrozenExceptTagged--;
                GameController.lastFrozenCharacter = this.gameObject;
                GameController.lastTaggedCharacter = col.gameObject;
            }
        }
        // If this unit collides with a not frozen player and this unit IS frozen...
        else if (col.gameObject.tag == "Not Frozen" && this.tag == "Frozen")
        {
            // Unfreeze it and change its tag
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.tag = "Not Frozen";
            if (GameController.currentState == GameController.ModeState.KINEMATIC)
            {
                // In the event of this being the Kinematic movement...
                // Reset the target of the not frozen unit to null, to allow to Wander again
                col.GetComponent<Seek>().target = null;
            }
            // Now handle GameController settings changes
            GameController.numNotFrozenExceptTagged++;
            if (GameController.lastFrozenCharacter == this.gameObject)
            {
                GameController.lastFrozenCharacter = null;
            }
        }
    }
}
