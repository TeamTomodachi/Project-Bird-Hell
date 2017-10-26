using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {
	public float CursorMovementSpeed = 550.0f;

	public PlayerPanelController PlayerPanel;
	public Image CursorImage;
	public RectTransform parentTransform;

	// Use this for initialization
	void Start () {
		parentTransform = transform.parent as RectTransform;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalMovement = Input.GetAxis(PlayerPanel.PlayerInfo.JoystickInputManagerPrefix + "Horizontal");
		float verticalMovement = Input.GetAxis(PlayerPanel.PlayerInfo.JoystickInputManagerPrefix + "Vertical");
		var newPosition = transform.position + (new Vector3(horizontalMovement, verticalMovement) * Time.fixedDeltaTime) * CursorMovementSpeed;

		//Debug.Log(parentTransform.rect.ToString());

		var properXMin = parentTransform.rect.xMin + (parentTransform.rect.width / 2);
		var properXMax = parentTransform.rect.xMax + (parentTransform.rect.width / 2);
		if (newPosition.x > properXMax)
			newPosition.x = properXMax;
		else if (newPosition.x < properXMin)
			newPosition.x = properXMin;

		var properYMin = parentTransform.rect.yMin + (parentTransform.rect.height / 2);
		var properYMax = parentTransform.rect.yMax + (parentTransform.rect.height / 2);
		if (newPosition.y > properYMax)
			newPosition.y = properYMax;
		else if (newPosition.y < properYMin)
			newPosition.y = properYMin;

		transform.position = newPosition;
	}
}
