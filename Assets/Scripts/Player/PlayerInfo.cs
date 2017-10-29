using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public static List<PlayerInfo> PlayersInGame = new List<PlayerInfo>();

    [SerializeField]
    public int ID;
    public string JoystickInputManagerPrefix { get { return "P" + ID + "_"; } }
    public string JoystickButtonPrefix { get { return "joystick " + ID + " button "; } }

    [SerializeField]
    public Color EmbellishmentColor;

    public PlayerInfo() : this(1) { }
    public PlayerInfo(int id)
    {
        ID = id;
    }
}
