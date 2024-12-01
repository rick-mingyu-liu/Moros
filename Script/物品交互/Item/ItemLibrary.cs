using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ItemLibrary", menuName = "ItemLibrary/New ItemLibrary")]
public class ItemLibrary : ScriptableObject
{
    [SerializeField]
    public List<int> index = new List<int>();
    [SerializeField]
    public List<Item> itemList = new List<Item>();

    public List<int> Index { get => index; set => index = value; }
    public List<Item> ItemList { get => itemList; set => itemList = value; }
}
