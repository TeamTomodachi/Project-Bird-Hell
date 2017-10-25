using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour {

	// Player Controllers
	public List<PlayerPanelController> PlayerPanels = new List<PlayerPanelController>();

	// Assets
	public CursorController CursorPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Gather all the joined players
		List<PlayerPanelController> joinedPlayers = new List<PlayerPanelController>();
		foreach (var panel in PlayerPanels)
		{
			if (panel.HasJoinedGame)
				joinedPlayers.Add(panel);
		}

		// Check to make sure they are all ready
		bool readyToStart = true;
		if (joinedPlayers.Count >= 2)
		{
			foreach (var panel in joinedPlayers)
			{
				if (!panel.IsReady)
				{
					readyToStart = false;
					break;
				}
			}
		}
		else { readyToStart = false; }

		// If we are ready to start, begin/check the countdown timer
		if (readyToStart)
		{

		}
	}
}
