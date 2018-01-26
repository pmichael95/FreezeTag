using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject[] players;

	// Use this for initialization
	void Awake () {
        int TaggedPlayer = Random.Range(1, players.Length);
        Debug.Log("Random int: " + TaggedPlayer);
        players[TaggedPlayer-1].tag = "Tagged Player";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject[] GetPlayers() {
        return players;
    }
}
