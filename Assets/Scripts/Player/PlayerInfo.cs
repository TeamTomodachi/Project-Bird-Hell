using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    [SerializeField]
    public int ID;

    [SerializeField]
    public PlayerInputType InputType;

    public string JoystickInputManagerPrefix { get { return "P" + ID + "_"; } }
    public string JoystickButtonPrefix { get { return "joystick " + ID + " button "; } }

    [SerializeField]
    public int BirdID;

    [SerializeField]
    public Color EmbellishmentColor;

    public PlayerInfo() : this(1) { }
    public PlayerInfo(int playerID) : this(playerID, 0) { }
    public PlayerInfo(int playerID, int birdID)
    {
        ID = playerID;
        BirdID = birdID;
    }
}

public enum PlayerInputType
{
    None            = -1,
    KeyboardMouse   = 0,
    Controller1     = 1,
    Controller2     = 2,
    Controller3     = 3,
    Controller4     = 4
}