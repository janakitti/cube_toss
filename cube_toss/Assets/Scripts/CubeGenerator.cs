using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour {

    public Rigidbody cube;
    public float initForce = -500f;
	
	void Update () {
		if (Input.GetKeyDown("c"))
        {
            Rigidbody instance = Instantiate(cube, new Vector3(0f, 10f, 0), new Quaternion(0, 0, 0, 0));
            instance.AddForce(new Vector3(0, initForce, 0));
        }
	}
}
