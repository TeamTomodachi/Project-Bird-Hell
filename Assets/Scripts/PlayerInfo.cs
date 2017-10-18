using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    [SerializeField]
    public int ID;
    public string ControllerPrefix { get { return "P" + ID + "_"; } }

    public PlayerInfo() : this(1) { }
    public PlayerInfo(int id)
    {
        ID = id;
    }
}
