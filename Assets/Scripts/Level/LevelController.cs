using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{
    public GameController Game { get; set; }
    public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();

    // Use for pre-start initialization
    public virtual void Awake()
    {
        Game = FindObjectOfType<GameController>();

        var spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (var s in spawnPoints)
        {
            var spawnPoint = s.GetComponent<SpawnPoint>();
            SpawnPoints.Add(spawnPoint);
        }
    }

    // Use this for initialization
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual SpawnPoint GetRandomSpawn()
    {
        int index = Game.Randomizer.Next(SpawnPoints.Count);
        return SpawnPoints[index];
    }

    public virtual IEnumerable<SpawnPoint> GetRandomSpawn(int number)
    {
        List<SpawnPoint> copySpawn = new List<SpawnPoint>(SpawnPoints);
        List<SpawnPoint> tmp = new List<SpawnPoint>();

        for (int i = 0; i < number + 1; i++)
        {
            int index = Game.Randomizer.Next(SpawnPoints.Count);
            tmp.Add(copySpawn[index]);
            copySpawn.RemoveAt(index);
        }

        return tmp;
    }

}
