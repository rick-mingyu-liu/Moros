using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWayOpenCheck : MonoBehaviour
{
    [Header("-- Player Data --")]
    [SerializeField] SaveItem gamesave;

    public GameEvent _event;
    public GameObject player;
    public BoxCollider2D boxCollider2D;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (_event.hasaved)
        {
            boxCollider2D.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boxCollider2D.enabled = true;

            _event.hasaved = true;
            gamesave.Save();
        }
    }

}
