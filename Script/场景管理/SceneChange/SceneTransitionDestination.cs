using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        A, B, C, D, E, F, G
    }

    public DestinationTag destinationTag;
    [Tooltip("This is the gameobject that has transitioned. For example, the player.")]
    public GameObject transitioningGameObject;
    public UnityEvent OnReachDestination;

    // Call this method when the object reaches the destination
    public void OnPlayerReach()
    {
        OnReachDestination?.Invoke();
    }
}
