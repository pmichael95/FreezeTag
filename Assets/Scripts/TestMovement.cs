using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {

    Vector3 movementZ = new Vector3(0.0f, 0.0f, 0.025f);
    Vector3 movementX = new Vector3(0.025f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            // Move forward
            this.transform.position = this.transform.position + movementZ;
        } 
        if (Input.GetKey(KeyCode.S))
        {
            // Move backwards
            this.transform.position = this.transform.position - movementZ;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Strafe left
            this.transform.position = this.transform.position - movementX;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // Strafe right
            this.transform.position = this.transform.position + movementX;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            // Rotate to the left
            this.transform.Rotate(new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * 50.0f, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            // Rotate to the left
            this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * 50.0f, Space.World);
        }
	}
}
