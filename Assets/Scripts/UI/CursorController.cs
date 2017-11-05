using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {
	public float CursorMovementSpeed = 550.0f;

	public IPlayerPanel PlayerPanel;
	public Image CursorImage;

	private RectTransform m_parentTransform;

	// Use this for initialization
	void Start () {
		m_parentTransform = transform.parent as RectTransform;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalMovement = Input.GetAxis(PlayerPanel.PlayerInformation.JoystickInputManagerPrefix + "Horizontal");
		float verticalMovement = Input.GetAxis(PlayerPanel.PlayerInformation.JoystickInputManagerPrefix + "Vertical");
		var newPosition = transform.position + (new Vector3(horizontalMovement, verticalMovement) * Time.fixedDeltaTime) * CursorMovementSpeed;

		//Debug.Log(m_parentTransform.rect.ToString());

		var properXMin = m_parentTransform.rect.xMin + (m_parentTransform.rect.width / 2);
		var properXMax = m_parentTransform.rect.xMax + (m_parentTransform.rect.width / 2);
		if (newPosition.x > properXMax)
			newPosition.x = properXMax;
		else if (newPosition.x < properXMin)
			newPosition.x = properXMin;

		var properYMin = m_parentTransform.rect.yMin + (m_parentTransform.rect.height / 2);
		var properYMax = m_parentTransform.rect.yMax + (m_parentTransform.rect.height / 2);
		if (newPosition.y > properYMax)
			newPosition.y = properYMax;
		else if (newPosition.y < properYMin)
			newPosition.y = properYMin;

		transform.position = newPosition;
	}
}
