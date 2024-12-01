using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SisterNotes : MonoBehaviour
{
    public string noteContent; // 笔记内容
    private bool isPlayerNearby = false; // 玩家是否在交互范围内
    private bool isNoteInInventory = false; // 笔记是否已在背包中

    public GameObject inventoryUI; // 背包UI，用于显示笔记
    public GameObject notePrefab; // 笔记在背包中的物体
    public Text noteContentText; // UI文本，用于显示笔记内容

    // Update is called once per frame
    private void Update()
    {
        // 检测玩家按下E键并在交互范围内
        if (isPlayerNearby && !isNoteInInventory && Input.GetKeyDown(KeyCode.E))
        {
            AddNoteToInventory();
        }
    }

    private void AddNoteToInventory()
    {
        // 创建笔记并加入背包
        GameObject newNote = Instantiate(notePrefab, inventoryUI.transform);
        newNote.GetComponent<Button>().onClick.AddListener(() => ShowNoteContent());
        isNoteInInventory = true;

        Debug.Log("Noted Added");
    }

    private void ShowNoteContent()
    {
        // 显示笔记内容
        noteContentText.text = noteContent;
        Debug.Log(noteContent);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Press E to pick up");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Away from Note");
        }
    }
}


// 配置步骤
/*
1. 笔记物体
    - 在场景中添加一个代表笔记的2D物体
    - 添加 SisterNotes脚本到该物体

2. 背包UI
    - 创建一个用于背包的Canvas UI, 包含一个垂直布局组（Vertical Layout Group) 用于显示笔记
    - 创建一个预制笔记按钮(notePrefab)，并关联到SisterNotes的notePrefab字段

3. 显示笔记内容
    - 在Canvas中添加一个Text组件，用于显示笔记内容
    - 将Text组件拖入SisterNotes的noteContentText字段

4. 玩家设置
    - 确保玩家物体有一个带有“player”标签的Collider2D，用于触发交互

5. 功能测试
    - 确保按下E键时，笔记会加入背包，并点击背包中的笔记按钮后显示内容
*/