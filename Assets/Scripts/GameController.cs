using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Tooltip("All possible players (units) in the game.")]
    public GameObject[] players;

    [Tooltip("The mode displayed text to inform the player of the current behavior state.")]
    public Text modeText;

    // ENUM to determine current behavior state
    public enum ModeState { KINEMATIC, STEERING };

    // Static used in identifying for other scripts and to swap display
    public static ModeState currentState;

	// Use this for initialization
	void Awake () {
        int TaggedPlayer = Random.Range(1, players.Length);
        players[TaggedPlayer-1].tag = "Tagged Player";
        if(!modeText) {
            modeText = GameObject.Find("CurrentMode").GetComponent<Text>();
        }
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
    }

    public GameObject[] GetPlayers() {
        return players;
    }
}
