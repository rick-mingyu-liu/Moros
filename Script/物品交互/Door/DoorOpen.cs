using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [Header("-- Player Data --")]
    [SerializeField] SaveItem gamesave;

    public GameEvent _event;
    public GameObject door;
    public GameObject finalPoint;
    public GameObject player;
    public Item slotItem1;
    public Item slotItem2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Load the event state at the start
        if (PlayerPrefs.HasKey($"GameEvent_{_event.Eventid}_hasaved"))
        {
            _event.hasaved = PlayerPrefs.GetInt($"GameEvent_{_event.Eventid}_hasaved") == 1;
        }
        else
        {
            _event.hasaved = false;
        }

        // Continue your original logic
        if (_event.hasaved && _event.Eventid == 0)
        {
            door.transform.localScale = finalPoint.transform.localScale;
        }
        if (_event.hasaved && _event.Eventid == 1)
        {
            door.transform.position = finalPoint.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && slotItem1.havePickup && _event.Eventid == 0) //�Լ���ҳ��б�ʯ��
        {
            door.transform.localScale = new Vector3(Mathf.Lerp(door.transform.localScale.x, finalPoint.transform.localScale.x, 0.1F),
                Mathf.Lerp(door.transform.localScale.y, finalPoint.transform.localScale.y, 0.1F),
                Mathf.Lerp(door.transform.localScale.z, finalPoint.transform.localScale.z, 0.1F));
            Action();
        }
        if (collision.gameObject.CompareTag("Player") && slotItem2.havePickup && _event.Eventid == 1) //�Լ���ҳ��б�ʯ��
        {
            door.transform.position = new Vector3(Mathf.Lerp(door.transform.position.x, finalPoint.transform.position.x, 0.1F),
                Mathf.Lerp(door.transform.position.y, finalPoint.transform.position.y, 0.1F),
                Mathf.Lerp(door.transform.position.z, finalPoint.transform.position.z, 0.1F));
            Action();
        }
    }

    public void Action()
    {
        _event.hasaved = true;
        gamesave.Save();
        // Save the event state to PlayerPrefs or another system
        PlayerPrefs.SetInt($"GameEvent_{_event.Eventid}_hasaved", _event.hasaved ? 1 : 0);
        PlayerPrefs.Save();
    }
}
