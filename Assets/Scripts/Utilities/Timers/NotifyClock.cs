using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyClock : MonoBehaviour, IClockTimer
{
    public Text UILabel;

    public event TimeEventHandler Tick;

    public TimeState ClockTimerState { get; private set; }
    public float CurrentTime { get; set; }
    public float TickLength { get; set; }
    public float LastTick { get; set; }
    public float NextTick { get { return LastTick + TickLength; } }

    public NotifyClock()
    {
        TickLength = 1.0f;
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

        // Update the Clock
        CurrentTime += Time.deltaTime;

        // Tick the Clock
        if (CurrentTime >= NextTick)
        {
            LastTick = NextTick;
            if (Tick != null)
                Tick(this, new TimeEventArgs(CurrentTime));
        }
    }

    public void StopClockTimer()
    {
        ClockTimerState = TimeState.Stopped;
        CurrentTime = 0.0f;
        LastTick = 0.0f;
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
