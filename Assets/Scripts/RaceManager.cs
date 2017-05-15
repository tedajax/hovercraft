using System;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public GameObject checkpointPrefab;
    public ObjectiveArrowController arrowController;

    private RacingCheckpoint currentCheckpoint;

    void Start()
    {
        createNextCheckpoint();
    }

    private void createNextCheckpoint()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-400f, 400f), 0f, UnityEngine.Random.Range(-400f, 400f));
        var checkpointObj = Instantiate(checkpointPrefab, position, Quaternion.identity) as GameObject;
        currentCheckpoint = checkpointObj.GetComponent<RacingCheckpoint>();

        currentCheckpoint.OnCheckpointTagged += checkpointTagged;

        arrowController.targetTransform = currentCheckpoint.transform;
    }

    private void checkpointTagged(GameObject tagger)
    {
        Destroy(currentCheckpoint.gameObject);
        createNextCheckpoint();
    }
}