using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToMovingObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PreviousPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (OnPlatform) {
			var deltaPosition = transform.position - PreviousPosition;
			deltaPosition.z = 0f;
			player.transform.position += deltaPosition;
		}
		PreviousPosition = gameObject.transform.position;
	}

	private Vector3 PreviousPosition;

	private bool OnPlatform;

	private GameObject player;

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			OnPlatform = true;
			player = collision.gameObject;
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player")
			OnPlatform = false;
	}
}
