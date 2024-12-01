using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossChargeBattle : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;

    public Item slotItem1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(slotItem1.havePickup)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        boss.SetActive(true);
        player.GetComponent<Player>().moveSpeed += 1.0f;
    }
}
