using UnityEngine;
using System;

public class RacingCheckpoint : MonoBehaviour
{
    public delegate void CheckpointTagged(GameObject tagger);

    public event CheckpointTagged OnCheckpointTagged;

    void OnTriggerEnter(Collider collider)
    {
        if (OnCheckpointTagged != null) {
            OnCheckpointTagged(collider.gameObject);
        }
    }
}