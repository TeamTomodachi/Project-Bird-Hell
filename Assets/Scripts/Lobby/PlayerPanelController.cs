using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelController : MonoBehaviour
{
	public PlayerInfo PlayerInfo = new PlayerInfo();
	//public Color CursorColor;

	private LobbyController Lobby;
	private CursorController Cursor;
	private GameObject StartToJoinPanel;
	private GameObject JoinedGamePanel;

	[HideInInspector]
	public bool IsReady = false;

	public bool HasJoinedGame
	{
		get
		{
			return JoinedGamePanel.activeSelf;
		}
	}

	// Use this for initialization
	void Start()
	{
		Lobby = GetComponentInParent<LobbyController>();
		StartToJoinPanel = this.gameObject.transform.GetChild(0).gameObject;
		JoinedGamePanel = this.gameObject.transform.GetChild(1).gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		// Start Button
#if UNITY_EDITOR_WIN
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 7))
#elif UNITY_EDITOR_OSX
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 9))
#else
		if (false)
#endif
		{
			JoinGame();
		}

		// Back Button
#if UNITY_EDITOR_WIN
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 6))
#elif UNITY_EDITOR_OSX
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 10))
#else
		if (false)
#endif
		{
			LeaveGame();
		}

		// X Button
#if UNITY_EDITOR_WIN
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 2))
#elif UNITY_EDITOR_OSX
		if (Input.GetKeyDown(PlayerInfo.JoystickButtonPrefix + 18))
#else
		if (false)
#endif
		{
			ToggleReady();
		}
	}

	public void JoinGame()
	{
		if (HasJoinedGame) return;
		StartToJoinPanel.SetActive(false);
		JoinedGamePanel.SetActive(true);

		// Unready
		IsReady = false;

		// Create the Cursor
		Cursor = Instantiate<CursorController>(Lobby.CursorPrefab, transform.position, transform.rotation);
		Cursor.transform.SetParent(Lobby.transform);
		Cursor.PlayerPanel = this;
		Cursor.CursorImage.color = PlayerInfo.EmbellishmentColor;
	}
	public void LeaveGame()
	{
		if (!HasJoinedGame) return;
		StartToJoinPanel.SetActive(true);
		JoinedGamePanel.SetActive(false);

		// Unready
		IsReady = false;

		// Destroy the Cursor
		Destroy(Cursor.gameObject);
	}

	public void ToggleReady()
	{
		if (!HasJoinedGame) return;
		IsReady = !IsReady;
	}
}
