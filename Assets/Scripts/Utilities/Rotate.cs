using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float XSpeed;
	public float YSpeed;
	public float ZSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(1, 0, 0), XSpeed * Time.deltaTime, Space.World);
		transform.Rotate(new Vector3(0, 1, 0), YSpeed * Time.deltaTime, Space.World);
		transform.Rotate(new Vector3(0, 0, 1), ZSpeed * Time.deltaTime, Space.World);
	}
}
