using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RaceManager raceManager;

    void Awake()
    {
        if (raceManager == null) {
            raceManager = GetComponent<RaceManager>();
        }
    }

    void Update()
    {

    }
}