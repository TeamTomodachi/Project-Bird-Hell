using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotifyClock))]
public class GameController : MonoBehaviour
{
    public List<PlayerController> PlayerPrefabs = new List<PlayerController>();
    public List<PlayerController> Players { get; set; }

    public System.Random Randomizer { get; set; }
    public LevelController Level { get; set; }
    public LevelNavigationParameter NavigationParameter { get; set; }
    public CameraController CameraController;
    public NotifyClock Clock;

    public GameController()
    {
        Randomizer = new System.Random();
        Players = new List<PlayerController>();
    }

    private void Awake()
    {
        // Grab the Navigation Parameter
        NavigationParameter = NavigationManager.Instance.Parameter as LevelNavigationParameter;
        if (NavigationParameter == null) NavigationParameter = new LevelNavigationParameter();

        // Grab the Level
        Level = FindObjectOfType<LevelController>();
    }

    // Use this for initialization
    void Start()
    {
        // Setup the Game Clock
        Clock.StopClockTimer();
        Clock.OnTick += Clock_OnTick;
        Clock.OnStart += Clock_OnStart;
        Clock.OnStop += Clock_OnStop;
        Clock.StartClockTimer();

        // Instantiate new players
        var randomSpawns = Level.GetRandomSpawn(NavigationParameter.PlayersInGame.Count);
        for (int i = 0; i < NavigationParameter.PlayersInGame.Count; i++)
        {
            // Cache the current Player Info
            var pInfo = NavigationParameter.PlayersInGame[i];

            // Create the Player
            CreatePlayer(PlayerPrefabs[0], pInfo, randomSpawns[i]);
        }

        // For debugging purposes... If the players count is 0, then we will create a player with the first bird prefab
        if (Players.Count == 0)
        {
            CreatePlayer(PlayerPrefabs[0], new PlayerInfo(), Level.GetRandomSpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private PlayerController CreatePlayer(PlayerController prefab, PlayerInfo pInfo, SpawnPoint spawn)
    {
        // Create the Player
        PlayerController player = Instantiate<PlayerController>(prefab);
        player.Info = pInfo;
        player.transform.position = spawn.transform.position;

        // Add the player to the Game
        Players.Add(player);
        CameraController.m_Targets.Add(player.transform);

        // Return the player
        return player;
    }

    private void Clock_OnStop(IClockTimer sender, TimeEventArgs e)
    {
    }

    private void Clock_OnStart(IClockTimer sender, TimeEventArgs e)
    {
    }

    private void Clock_OnTick(IClockTimer sender, TimeEventArgs e)
    {
    }
}
