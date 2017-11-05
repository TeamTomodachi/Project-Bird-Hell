using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelController : MonoBehaviour
{
    public List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();

    // Use for pre-start initialization
    public virtual void Awake()
    {
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

}
