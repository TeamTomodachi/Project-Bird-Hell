using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIPanelController : MonoBehaviour {
	public PlayerController Player;
	public Image heroImg;
	public RectTransform livesPanel;
	public RectTransform staminaPanel;
	public GameObject lifePrefab;

	private RectTransform m_parent_staminaPanel;
	// Use this for initialization
	void Start () {
		m_parent_staminaPanel = staminaPanel.parent as RectTransform;
	}
	
	// Update is called once per frame
	void Update () {
		// Update the Stamina Bar
		var staminaPercentage = Player.Stamina.StaminaPercentage;                               // Calculate the Percentage of stamina remaining
		var newStaminaBarWidth = m_parent_staminaPanel.rect.width * staminaPercentage;          // Calculate the New width off using the percentage
		newStaminaBarWidth = m_parent_staminaPanel.rect.width - newStaminaBarWidth;             // Inverse the width so it is 0 based climbing to max 
		staminaPanel.offsetMax = new Vector2(-newStaminaBarWidth, staminaPanel.offsetMax.y);    // Set the Right value
	}
}
