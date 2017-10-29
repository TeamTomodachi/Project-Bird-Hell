using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	List<PlayerController> Players = new List<PlayerController>();

	// Use this for initialization
	void Start () {
		// Instantiate new players
		foreach (var pInfo in PlayerInfo.PlayersInGame)
		{
			//PlayerController player; //= Instantiate<PlayerController>();
			//player.Info = pInfo;
			//Players.Add(player);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
