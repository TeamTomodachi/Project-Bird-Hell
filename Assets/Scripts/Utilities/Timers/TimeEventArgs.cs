using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TimeEventHandler(IClockTimer sender, TimeEventArgs e);
public class TimeEventArgs : EventArgs {
    public float CurrentTime { get; private set; }
    public bool StateChanged { get; private set; }

    public TimeEventArgs(float currentTime) : this(currentTime, false) { }
    public TimeEventArgs(float currentTime, bool stateChanged)
    {
        CurrentTime = currentTime;
        StateChanged = stateChanged;
    }
}