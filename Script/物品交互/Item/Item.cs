using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ItemLibrary/New Item")]
public class Item : ScriptableObject
{
    [Header("ID")]
    public int Itemid;

    [Header("ItemInfo")]
    public Sprite itemImage;
    [TextArea]
    public string ItemInfo;
    [TextArea]
    public string ItemContent;

    public bool havePickup;
}
