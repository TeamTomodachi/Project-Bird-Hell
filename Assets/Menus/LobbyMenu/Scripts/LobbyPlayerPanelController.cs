using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerPanelController : MonoBehaviour, IPlayerPanel
{
	public PlayerInfo PlayerInfo = new PlayerInfo();
	public PlayerInfo PlayerInformation { get { return PlayerInfo; } }

	private LobbyController Lobby { get; set; }
	private LobbyLevelSelectPanelController VoteCast { get; set; }

	// Self-References
	private CursorController Cursor { get; set; }
	private GameObject StartToJoinPanel { get; set; }
	private GameObject JoinedGamePanel { get; set; }
	private Image m_image { get; set; }


	public bool IsReady { get; set; }
	public bool HasJoinedGame
	{
		get
		{
			return JoinedGamePanel.activeSelf;
		}
	}

	public LobbyPlayerPanelController()
	{
		IsReady = false;
	}

	// Use this for initialization
	void Start()
	{
		Lobby = GetComponentInParent<LobbyController>();
		StartToJoinPanel = this.gameObject.transform.GetChild(0).gameObject;
		JoinedGamePanel = this.gameObject.transform.GetChild(1).gameObject;

		// Set the color on the Image
		m_image = GetComponent<Image>();
		m_image.color = PlayerInfo.EmbellishmentColor;
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
		Cursor.OnClick += Cursor_OnClick;
	}

	private void Cursor_OnClick(object sender, CursorEventArgs e)
	{
		//Debug.Log("Casting Vote at position" + e.Position);
		foreach (var level in Lobby.LevelSelectionPanels)
		{
			if (level.ContainsPosition(e.Position))
			{
				Debug.Log("Vote Cast for: " + level.Level.Name);

				// If we have already cast a vote, get rid of it
				if (VoteCast != null)
				{
					VoteCast.TotalVotes--;
				}

				// Cast the vote for the new item
				VoteCast = level;
				VoteCast.TotalVotes++;
				break;
			}
		}
	}

	public void LeaveGame()
	{
		if (!HasJoinedGame) return;
		StartToJoinPanel.SetActive(true);
		JoinedGamePanel.SetActive(false);

		// If we have already cast a vote, get rid of it
		if (VoteCast != null)
		{
			VoteCast.TotalVotes--;
		}

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
