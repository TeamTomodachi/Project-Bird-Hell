using System;
using System.Collections;
using System.Collections.Generic;

public interface IClockTimer
{
    event TimeEventHandler OnTick;
    event TimeEventHandler OnStart;
    event TimeEventHandler OnPause;
    event TimeEventHandler OnStop;

    TimeState ClockTimerState { get; }
    TimeSpan CurrentTimeSpan { get; }
    float CurrentTime { get; }
    float TickLength { get; set; }
    float LastTick { get; }
    float NextTick { get; }


    void StartClockTimer();
    void PauseClockTimer();
    void StopClockTimer();
}

public enum TimeState
{
    None,
    Stopped,
    Started,
    Paused,
    Completed
}

