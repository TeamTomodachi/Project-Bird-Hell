using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {
	public const float CURSOR_MOVEMENT_SPEED = 200.0f;

	public PlayerPanelController PlayerPanel;
	public Image CursorImage;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalMovement = Input.GetAxis(PlayerPanel.PlayerInfo.JoystickInputManagerPrefix + "Horizontal");
		float verticalMovement = Input.GetAxis(PlayerPanel.PlayerInfo.JoystickInputManagerPrefix + "Vertical");
		transform.position += (new Vector3(horizontalMovement, verticalMovement) * Time.fixedDeltaTime) * CURSOR_MOVEMENT_SPEED;
	}
}
