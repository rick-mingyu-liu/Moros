using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameEvent", menuName = "GameEvent/New GameEvent")]
public class GameEvent : ScriptableObject
{
    [Header("ID")]
    public int Eventid;

    public bool hasaved;
}
