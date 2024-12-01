using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class ItemSlot : MonoBehaviour
{
    [Header("MemoryInfo")]
    public int slotID;//Space ID is equal to the Item ID
    public Item slotItem;
    public Image slotImage;
    public string slotInfo;
    public string slotContent;
    public UnityEvent _event1;
    public UnityEvent _event2;
    public UnityEvent _event3;
    public UnityEvent _event4;

    private void Awake()
    {
        slotID = slotItem.Itemid;

        if (slotItem.havePickup)
        {
            slotInfo = slotItem.ItemInfo;
            slotImage.sprite = slotItem.itemImage;
            slotContent = slotItem.ItemContent;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _event1.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _event2.Invoke();
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _event3.Invoke();
        }else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            _event4.Invoke();
        }
    }


    public void ItemOnClicked()
    {
        if (slotItem.havePickup)
        {
            slotInfo = slotItem.ItemInfo;
            ItemManager.UpdateItemInfo(slotInfo);
        }
    }

    public void ItemOnClickedRight()
    {
        if (slotItem.havePickup)
        {
            slotContent = slotItem.ItemContent;
            ItemManager.UpdateItemContent(slotContent);
        }
    }
}
