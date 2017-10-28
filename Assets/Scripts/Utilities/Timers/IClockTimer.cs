using System;
using System.Collections;
using System.Collections.Generic;

public interface IClockTimer
{
    event TimeEventHandler Tick;

    TimeState ClockTimerState { get; }
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
    Stopped,
    Started,
    Paused,
    Completed
}

