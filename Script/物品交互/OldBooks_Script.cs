using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldbooks : MonoBehaviour
{
    public string bookContent; // 古老书籍内容
    private bool isNearPlayer = false;

    // Update is called once per frame
    private void Update()
    {
        // 检测玩家按下E键并靠近书籍
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            AddToInventory();
        }
    }

    private void AddToInventory()
    {
        // 将书籍添加到背包
        InventoryManager.Instance.AddBookToInventory(this);
        // 隐藏书籍（模拟拾取）
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}


// 背包UI管理
// 在Unity编辑器中创建以下内容
/*
1. UI结构
    - 背包面板(InventoryPanel)  
        - 子物体Content（用于显示书籍按钮的区域）
        - 子物体BookContentPanel（显示书籍内容的区域，包含一个Text元素）

2. 预制体
    - 创建一个书籍按钮预制体BookButton，包含一个按钮组件和一个文本组件

3. 挂载脚本
    - 将InventoryManager脚本挂载到一个空的GameObject上
    - 在InventoryManager的Inspector面板中，设置inventoryUI，inventoryContent和bookButtonPrefab
*/