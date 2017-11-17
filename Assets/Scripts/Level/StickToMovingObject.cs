using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToMovingObject : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PreviousPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var deltaPosition = transform.position - PreviousPosition;
        deltaPosition.z = 0f;

        foreach (var player in PlayersOnPlatform)
        {
            player.transform.position += deltaPosition;
        }

        PreviousPosition = gameObject.transform.position;
    }

    private Vector3 PreviousPosition;
    public List<GameObject> PlayersOnPlatform = new List<GameObject>();
    public void BeginTracking(GameObject gObj)
    {
        if (gObj.tag == "Player")
        {
            PlayersOnPlatform.Add(gObj);
        }
    }
    public void EndTracking(GameObject gObj)
    {
        if (gObj.tag == "Player")
        {
            PlayersOnPlatform.Remove(gObj);
        }
    }

    // Collision Events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BeginTracking(collision.gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        EndTracking(collision.gameObject);
    }

    // Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BeginTracking(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        EndTracking(collision.gameObject);
    }
}
