using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour, IClockTimer
{
    public event TimeEventHandler OnCompleted;
    public event TimeEventHandler OnTick;
    public event TimeEventHandler OnStart;
    public event TimeEventHandler OnPause;
    public event TimeEventHandler OnStop;

    public TimeState ClockTimerState { get; private set; }
    public TimeSpan CurrentTimeSpan { get { return TimeSpan.FromSeconds(CurrentTime); } }
    public float CurrentTime { get; set; }
    public float TickLength { get; set; }
    public float LastTick { get; set; }
    public float NextTick { get { return LastTick - TickLength; } }

    public float StartTime = 10.0f;

    public CountdownTimer()
    {
        TickLength = 1.0f;
        StartTime = 10.0f;
        ClockTimerState = TimeState.None;
        CurrentTime = StartTime;
        LastTick = StartTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer is started
        if (ClockTimerState != TimeState.Started) return;

        // Update the Timer
        CurrentTime -= Time.deltaTime;

        // Tick the Timer
        if (CurrentTime <= NextTick)
        {
            LastTick = NextTick;
            if (OnTick != null)
                OnTick(this, new TimeEventArgs(CurrentTime));
        }

        // Notify Completion
        if (CurrentTime <= 0.0f)
        {
            ClockTimerState = TimeState.Completed;
            CurrentTime = 0.0f;
            if (OnCompleted != null)
                OnCompleted(this, new TimeEventArgs(CurrentTime));
        }
    }

    public void StopClockTimer()
    {
        if (ClockTimerState == TimeState.Stopped) return;

        ClockTimerState = TimeState.Stopped;
        CurrentTime = StartTime;
        LastTick = StartTime;

        if (OnStop != null)
            OnStop(this, new TimeEventArgs(CurrentTime, true));
    }

    public void StartClockTimer()
    {
        if (ClockTimerState == TimeState.None) return;
        if (ClockTimerState == TimeState.Started) return;
        if (ClockTimerState == TimeState.Completed) return;

        ClockTimerState = TimeState.Started;
        if (OnStart != null)
            OnStart(this, new TimeEventArgs(CurrentTime, true));
    }

    public void PauseClockTimer()
    {
        if (ClockTimerState != TimeState.Started) return;


        ClockTimerState = TimeState.Paused;
        if (OnPause != null)
            OnPause(this, new TimeEventArgs(CurrentTime, true));
    }
}
