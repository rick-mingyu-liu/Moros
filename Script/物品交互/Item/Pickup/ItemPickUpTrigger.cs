using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickUpTrigger : MonoBehaviour
{
    public ItemLibrary itemBag;
    public Item item;

    [Header("TIMEEVENT")]
    public UnityEvent action;

    [Header("Tip")]
    private GameObject pickupTip;

    private void Awake()
    {
        if (itemBag.ItemList.Contains(item))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pickupTip = gameObject.transform.Find("PickupTip").gameObject;
        pickupTip.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickupTip.SetActive(true);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) &&
            collision.gameObject.CompareTag("Player"))
        {
            if (!itemBag.ItemList.Contains(item))
            {
                collision.GetComponent<DictionaryScript_Item>().OnBeforeSerialize();
                collision.GetComponent<DictionaryScript_Item>().DeserializeDictionary(item.Itemid, item);
                item.havePickup = true;
                AddNewItem();
                action.Invoke();
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickupTip.SetActive(false);
    }


    public void AddNewItem()
    {
        ItemManager.CompareItem(item);
    }

    public void ItemDestory()
    {
        Destroy(gameObject);
    }
}
