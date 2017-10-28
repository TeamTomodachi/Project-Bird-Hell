using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour, IClockTimer
{
    public Text UILabel;

    public event TimeEventHandler Tick;
    public event TimeEventHandler Completed;

    public TimeState ClockTimerState { get; private set; }
    public float CurrentTime { get; set; }
    public float TickLength { get; set; }
    public float LastTick { get; set; }
    public float NextTick { get { return LastTick - TickLength; } }

    public float StartTime = 10.0f;

    public CountdownTimer()
    {
        TickLength = 1.0f;
        StartTime = 10.0f;
        StopClockTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Label
        if (UILabel != null)
        {
            UILabel.text = CurrentTime.ToString();
        }

        // Check if the timer is started
        if (ClockTimerState != TimeState.Started) return;

        // Update the Timer
        CurrentTime -= Time.deltaTime;

        // Tick the Timer
        if (CurrentTime <= NextTick)
        {
            LastTick = NextTick;
            if (Tick != null)
                Tick(this, new TimeEventArgs(CurrentTime));
        }

        // Notify Completion
        if (CurrentTime <= 0.0f)
        {
            ClockTimerState = TimeState.Completed;
            CurrentTime = 0.0f;
            if (Completed != null)
                Completed(this, new TimeEventArgs(CurrentTime));
        }
    }

    public void StopClockTimer()
    {
        ClockTimerState = TimeState.Stopped;
        CurrentTime = StartTime;
        LastTick = StartTime;
    }

    public void StartClockTimer()
    {
        ClockTimerState = TimeState.Started;
    }

    public void PauseClockTimer()
    {
        ClockTimerState = TimeState.Paused;
    }
}
