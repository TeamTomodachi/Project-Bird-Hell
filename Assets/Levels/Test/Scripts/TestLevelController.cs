using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelController : LevelController
{
    public override void Start()
    {
        base.Start();

        var player = FindObjectOfType<PlayerController>();
        var randomSpawn = GetRandomSpawn();
        player.transform.position = randomSpawn.transform.position;
    }
    public override void Update()
    {
        base.Update();
    }
}
