using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryScript_Item : MonoBehaviour, ISerializationCallbackReceiver
{


    [SerializeField]
    private ItemLibrary dictionaryData;
    [SerializeField]
    public List<int> keys = new List<int>();
    [SerializeField]
    public List<Item> values = new List<Item>();

    private Dictionary<int, Item> myDictionary = new Dictionary<int, Item>();

    public bool modifyValues;

    public int currentcount;

    public void OnBeforeSerialize()
    {
        if (dictionaryData == null || dictionaryData.Index == null || dictionaryData.ItemList == null)
        {
            return;
        }
        if (modifyValues == false)
        {
            keys.Clear();
            values.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryData.Index.Count, dictionaryData.ItemList.Count); i++)
            {
                keys.Add(dictionaryData.Index[i]);
                values.Add(dictionaryData.ItemList[i]);
            }
            currentcount = Mathf.Min(dictionaryData.Index.Count, dictionaryData.ItemList.Count);
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void DeserializeDictionary()
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Index.Clear();
        dictionaryData.ItemList.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.ItemList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DeserializeDictionary(int key, Item value)
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Index.Clear();
        dictionaryData.ItemList.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.ItemList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        dictionaryData.Index.Add(key);
        dictionaryData.ItemList.Add(value);
        myDictionary.Add(key, value);
        modifyValues = false;
    }

    public void DeleteDictionary(int key, Item value)
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Index.Clear();
        dictionaryData.ItemList.Clear();
        keys.Remove(key);
        values.Remove(value);
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.ItemList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DestoryDictionary()
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Index.Clear();
        dictionaryData.ItemList.Clear();
        modifyValues = false;
    }

    public void ResetPickUp_Attribute()
    {
        for (int i = 0; i < dictionaryData.ItemList.Count; i++)
        {
            if (!dictionaryData.ItemList[i])
                return;
            dictionaryData.ItemList[i].havePickup = false;
        }
    }
}
