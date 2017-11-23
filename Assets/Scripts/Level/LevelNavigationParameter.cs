using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNavigationParameter {
    public List<PlayerInfo> PlayersInGame { get; set; }
    public LevelInfo SelectedLevel { get; set; }

    public LevelNavigationParameter()
    {
        PlayersInGame = new List<PlayerInfo>();
        SelectedLevel = new LevelInfo();
    }
}
