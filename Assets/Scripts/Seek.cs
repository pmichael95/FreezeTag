using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour {
    [Tooltip("The target to seek.")]
    public GameObject target;
    private GameObject[] players;
    float speed = 0.5f;

    void Start() {
    }

    void Update() {
        if (tag == "Tagged Player") {
            FindTarget();
        }

        // Give velocity at each frame adaptively
        if (target) {
            GetComponent<Rigidbody>().velocity = ((target.transform.position - transform.position).normalized * speed);
        }
    }

    private void FindTarget() {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;

        foreach (GameObject player in GameObject.Find("Game Controller").GetComponent<GameController>().GetPlayers()) {
            if(player != this.gameObject) {
                if(OldDistanceToPlayer != Vector3.zero) {
                    distanceToPlayer = (player.transform.position - transform.position).normalized;
                    if(distanceToPlayer.magnitude < OldDistanceToPlayer.magnitude) {
                        // Set the new target to the newly tested distance
                        target = player;
                    }
                }
                else {
                    OldDistanceToPlayer = (player.transform.position - transform.position).normalized;
                    // By default set it to the first one visited
                    target = player;
                }
            }
        }
    }
}
