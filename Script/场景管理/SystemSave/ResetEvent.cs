using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEvent : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;

    public List<GameEvent> gameEvent = new List<GameEvent>();


    private void Start()
    {
        ;
    }
    public void RestEvent_GameObject()
    {
        foreach (var _event in gameEvent)
        {
            _event.hasaved = false;
            bagsave.Save();
        }
    }
}
