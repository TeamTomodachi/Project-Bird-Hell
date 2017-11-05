using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    [SerializeField]
    public int ID;
    public string JoystickInputManagerPrefix { get { return "P" + ID + "_"; } }
    public string JoystickButtonPrefix { get { return "joystick " + ID + " button "; } }

    [SerializeField]
    public int BirdID;

    [SerializeField]
    public Color EmbellishmentColor;

    public PlayerInfo() : this(1) { }
    public PlayerInfo(int id)
    {
        ID = id;
    }
}
