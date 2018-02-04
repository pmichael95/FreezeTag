using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    #region GAME CONTROLLER VARIABLES
    [Tooltip("All possible players (units) in the game.")]
    public GameObject[] players;

    [Tooltip("The mode displayed text to inform the player of the current behavior state.")]
    public Text modeText;

    // ENUM to determine current behavior state
    public enum ModeState { KINEMATIC, STEERING };

    // Static used in identifying for other scripts and to swap display
    public static ModeState currentState;

    // The number of non-frozen characters, to unfreeze if all got frozen/only tagged player
    public static int numNotFrozenExceptTagged = 4;
    public static int MAX_NUM_PLAYERS_EXCEPT_TAGGED = 4;

    // The last frozen character
    public static GameObject lastFrozenCharacter = null;
    public static GameObject lastTaggedCharacter = null;

    #endregion

    // Initialize possible players
	void Awake () {
        int TaggedPlayer = Random.Range(1, players.Length);
        players[TaggedPlayer-1].tag = "Tagged Player";
        if(!modeText) {
            modeText = GameObject.Find("CurrentMode").GetComponent<Text>();
        }
        // Default state is KINEMATIC movement
        currentState = ModeState.KINEMATIC;
	}

    void Update() {
        #region MODE_CHANGING
        if (Input.GetKeyDown(KeyCode.T)) {
            if(currentState == ModeState.KINEMATIC) {
                currentState = ModeState.STEERING;
                modeText.text = "Steering";
            }
            else {
                currentState = ModeState.KINEMATIC;
                modeText.text = "Kinematic";
            }
        }
        #endregion

        // Tag changing and unfreezing to allow the game to continue when no more freezable characters exist
        #region UNFREEZING_FOR_CONTINUED_GAME
        if (numNotFrozenExceptTagged == 0 && lastFrozenCharacter)
        {
            lastFrozenCharacter.tag = "Tagged Player";
            lastFrozenCharacter.transform.position = new Vector3(0.0f, lastFrozenCharacter.transform.position.y, 5.0f);
            lastTaggedCharacter.transform.position = new Vector3(0.0f, lastTaggedCharacter.transform.position.y, -5.0f);
            lastFrozenCharacter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            lastTaggedCharacter.tag = "Not Frozen";
            lastTaggedCharacter.GetComponent<Seek>().target = null;
            lastTaggedCharacter.GetComponent<SteeringSeek>().target = null;
            numNotFrozenExceptTagged = 1;
            lastFrozenCharacter = null;
            lastTaggedCharacter = null;
        }
        #endregion
    }

    // Returns the possible players array for finding targets
    public GameObject[] GetPlayers() {
        return players;
    }
}
