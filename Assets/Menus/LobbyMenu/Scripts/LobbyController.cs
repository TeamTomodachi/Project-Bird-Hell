using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public int MinimumPlayersToStart = 2;

    // Player Controllers
    public List<LobbyPlayerPanelController> PlayerPanels = new List<LobbyPlayerPanelController>();
    public List<LobbyLevelSelectPanelController> LevelSelectionPanels = new List<LobbyLevelSelectPanelController>();

    // Countdown Timer
    public CountdownTimer StartTimer;
    public Text TimerUIText;

    // Prefabs
    public CursorController CursorPrefab;

    private void Awake()
    {
        StartTimer.StopClockTimer();
        StartTimer.OnTick += StartTimer_Tick;
        StartTimer.OnStart += StartTimer_OnStart;
        StartTimer.OnStop += StartTimer_OnStop;
        StartTimer.OnCompleted += StartTimer_Completed;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Gather all the joined players
        List<LobbyPlayerPanelController> joinedPlayers = new List<LobbyPlayerPanelController>();
        foreach (var panel in PlayerPanels)
        {
            if (panel.HasJoinedGame)
                joinedPlayers.Add(panel);
        }

        // Require a minimum of two players to start a game
        //Debug.Log("Total Joined Players: " + joinedPlayers.Count);
        bool readyToStart = true;
        if (joinedPlayers.Count >= MinimumPlayersToStart)
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

        // Create the Navigation Parameter
        LevelNavigationParameter navigationParameter = new LevelNavigationParameter();

        // Save the Players to static cache for recreation ingame
        foreach (var panel in PlayerPanels)
        {
            if (panel.HasJoinedGame)
                navigationParameter.PlayersInGame.Add(panel.PlayerInfo);
        }

        // Get the level vote winner
        LobbyLevelSelectPanelController levelMostVotes = LevelSelectionPanels[0];
        foreach (var level in LevelSelectionPanels)
        {
            if (level.TotalVotes > levelMostVotes.TotalVotes)
                levelMostVotes = level;
        }

        // If Random was selected, set a random one
        if (string.IsNullOrEmpty(levelMostVotes.Level.SceneName) || levelMostVotes.Level.Name == "Random") {
            var index = Random.Range(1, LevelSelectionPanels.Count);
            levelMostVotes = LevelSelectionPanels[index];
        }

        // Set the Selected Level
        navigationParameter.SelectedLevel = levelMostVotes.Level;

        // Begin loading the map
        NavigationManager.Instance.Navigate(navigationParameter.SelectedLevel.SceneName, navigationParameter);
    }
}
