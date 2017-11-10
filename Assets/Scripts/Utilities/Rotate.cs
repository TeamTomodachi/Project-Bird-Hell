using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public Quaternion StartingRotation { get; set; }
	public Quaternion LastLocalRotation { get; set; }
	public Quaternion LastRotation { get; set; }

	public Vector3 Speed;

	// Use this for initialization
	void Start () {
		StartingRotation = transform.localRotation;
	}

	// Update is called once per frame
	void Update () {
		// Save the old values for differences
		LastLocalRotation = transform.localRotation;
		LastRotation = transform.rotation;

		// Preform Rotations
		transform.Rotate(new Vector3(1, 0, 0), Speed.x * Time.deltaTime, Space.Self);
		transform.Rotate(new Vector3(0, 1, 0), Speed.y * Time.deltaTime, Space.Self);
		transform.Rotate(new Vector3(0, 0, 1), Speed.z * Time.deltaTime, Space.Self);
	}

	public void ResetRotation()
	{
		transform.localRotation = StartingRotation;
	}
}
