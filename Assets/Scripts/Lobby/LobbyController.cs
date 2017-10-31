using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{

    // Player Controllers
    public List<PlayerPanelController> PlayerPanels = new List<PlayerPanelController>();

    // Countdown Timer
    public CountdownTimer StartTimer;
    public Text TimerUIText;

    // Assets
    public CursorController CursorPrefab;

    // Use this for initialization
    void Start()
    {
        StartTimer.StopClockTimer();
        StartTimer.OnTick += StartTimer_Tick;
        StartTimer.OnStart += StartTimer_OnStart;
        StartTimer.OnStop += StartTimer_OnStop;
        StartTimer.OnCompleted += StartTimer_Completed;
    }

    // Update is called once per frame
    void Update()
    {
        // Gather all the joined players
        List<PlayerPanelController> joinedPlayers = new List<PlayerPanelController>();
        foreach (var panel in PlayerPanels)
        {
            if (panel.HasJoinedGame)
                joinedPlayers.Add(panel);
        }

        //Debug.Log("Total Joined Players: " + joinedPlayers.Count);

        // Require a minimum of two players to start a game
        bool readyToStart = true;
        if (joinedPlayers.Count >= 1)
        {
            // Check to make sure that all of the players are ready
            foreach (var panel in joinedPlayers)
            {
                //Debug.Log("Checking Ready Status - Controller: " + panel.PlayerInfo.ID);
                if (!panel.IsReady)
                {
                    //Debug.Log("Controller " + panel.PlayerInfo.ID + " Not Ready");
                    readyToStart = false;
                    break;
                }
            }
        }
        else { readyToStart = false; }

        // If we are ready to start, begin/check the countdown timer
        if (readyToStart)
        {
            //Debug.Log("Starting game timer");
            ((IClockTimer)StartTimer).StartClockTimer();
        }
        else
        {
            ((IClockTimer)StartTimer).StopClockTimer();
        }
    }

    private void StartTimer_OnStart(IClockTimer sender, TimeEventArgs e)
    {
        Debug.Log("Timer Started");
        var timer = sender as CountdownTimer;
        if (TimerUIText != null)
        {
            TimerUIText.text = (timer.CurrentTimeSpan.Seconds).ToString();
        }
    }

    private void StartTimer_OnStop(IClockTimer sender, TimeEventArgs e)
    {
        Debug.Log("Timer Started");
        //var timer = sender as CountdownTimer;
        if (TimerUIText != null)
        {
            TimerUIText.text = "";
        }
    }

    private void StartTimer_Tick(IClockTimer sender, TimeEventArgs e)
    {
        Debug.Log("Timer Tick");
        var timer = sender as CountdownTimer;

        if (TimerUIText != null)
        {
            if (timer.CurrentTime == timer.StartTime)
                TimerUIText.text = (timer.CurrentTimeSpan.Seconds).ToString();
            else if (timer.CurrentTime == 0.0f)
                TimerUIText.text = "0";
            else
                TimerUIText.text = (timer.CurrentTimeSpan.Seconds + 1).ToString();
        }
    }

    private void StartTimer_Completed(IClockTimer sender, TimeEventArgs e)
    {
        Debug.Log("Timer Complete");
        //var timer = sender as CountdownTimer;
        if (TimerUIText != null)
        {
            TimerUIText.text = "";
        }

        // Save the Players to static cache for recreation ingame
        PlayerInfo.PlayersInGame = new List<PlayerInfo>();
        foreach (var panel in PlayerPanels)
        {
            if (panel.HasJoinedGame)
                PlayerInfo.PlayersInGame.Add(panel.PlayerInfo);
        }

        // Begin loading the map
        SceneManager.LoadScene("Test");
    }
}
