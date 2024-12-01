using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConditionalPickup : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D boxCollider;
    public GameEvent _event;
    public CinemachineVirtualCamera keyCM;
    public Item slotItem1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if(_event && keyCM)
        {
            if (_event.hasaved && !player.GetComponent<DictionaryScript_Item>().keys.Contains(1))
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                boxCollider.enabled = true;
                GameObject.Find("BlendListCamera").GetComponent<CinemachineBlendListCamera>().m_Instructions[0].m_VirtualCamera = keyCM;
            }
        }
        else if (_event && !keyCM)
        {
            if (_event.hasaved)
            {
                boxCollider.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slotItem1)
        {
            if (slotItem1.havePickup)
            {
                boxCollider.enabled = true;
            }
        }
    }
}
