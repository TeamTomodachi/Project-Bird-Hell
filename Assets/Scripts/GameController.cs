using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotifyClock))]
public class GameController : MonoBehaviour
{
    public List<PlayerController> PlayerPrefabs = new List<PlayerController>();
    List<PlayerController> Players = new List<PlayerController>();

    public NotifyClock Clock;

    // Use this for initialization
    void Start()
    {
        Clock.StopClockTimer();
        Clock.OnTick += Clock_OnTick;
        Clock.OnStart += Clock_OnStart;
        Clock.OnStop += Clock_OnStop;
        Clock.StartClockTimer();

        // Instantiate new players
        foreach (var pInfo in PlayerInfo.PlayersInGame)
        {
            //PlayerController player; //= Instantiate<PlayerController>();
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
