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
    public CameraController CameraController;
    public NotifyClock Clock;

    public GameController()
    {
        Randomizer = new System.Random();
        Players = new List<PlayerController>();
    }

    private void Awake()
    {
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
        foreach (var pInfo in LobbyController.PlayersInGame)
        {
            //PlayerController player = null; //= Instantiate<PlayerController>();
            //player.Info = pInfo;
            //Players.Add(player);
        }
    }

    // Update is called once per frame
    void Update()
    {

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
