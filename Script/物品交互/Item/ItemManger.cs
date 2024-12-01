using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    static ItemManager instance;
    static DictionaryScript_Item my_item;
    public ItemLibrary myItemBag;
    public Text ItemInfromation;
    public Text ItemContent;
    public static GameObject Panel_ReadFile;
    public static bool isOpenPanel_ReadFile = false;
    public List<GameObject> items = new List<GameObject>();


    void Awake()
    {
        my_item = GetComponent<DictionaryScript_Item>();
        Panel_ReadFile = GameObject.Find("Panel_ReadFile");
        if(Panel_ReadFile)
        {
            //Debug.Log("find it Panel_ReadFile");
        }
        Panel_ReadFile.SetActive(false);
        
        if (instance != null)
            Destroy(this);
        instance = this;

        foreach (var Key in myItemBag.Index)
        {
            instance.items[Key].GetComponent<ItemSlot>().slotImage.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        instance.ItemInfromation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.ItemInfromation.text = itemDescription;
    }   
    
    public static void UpdateItemContent(string itemContent)
    {
        instance.ItemContent.text = itemContent;
        Panel_ReadFile.SetActive(isOpenPanel_ReadFile);
        isOpenPanel_ReadFile = !isOpenPanel_ReadFile;
    }

    public static void CompareItem(Item item)
    {
        instance.items[item.Itemid].GetComponent<ItemSlot>().slotImage.sprite = item.itemImage;
        instance.items[item.Itemid].GetComponent<ItemSlot>().slotImage.gameObject.SetActive(true);
    }
}
