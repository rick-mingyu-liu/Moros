using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public GameObject inventoryUI; // 背包UI
    public Transform inventoryContent; // 用于显示背包内容的区域
    public GameObject bookButtonPrefab; // 背包中书籍按钮的预制体
    public Text bookContentText; // 用于显示书籍内容的UI文本

    private List<Oldbooks> books = new List<Oldbooks>();

    private void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddBookToInventory(Oldbooks book)
    {
        books.Add(book);

        // 在背包UI中添加书籍按钮
        GameObject bookButton = Instantiate(bookButtonPrefab, inventoryContent);
        bookButton.GetComponentInChildren<Text>().text = "Old Book";
        bookButton.GetComponent<Button>().onClick.AddListener(() => ShowBookContent(book.bookContent));
    }

    public void ShowBookContent(string content)
    {
        // 显示书籍内容
        bookContentText.text = content;
        inventoryUI.SetActive(true);
    }

    public void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
