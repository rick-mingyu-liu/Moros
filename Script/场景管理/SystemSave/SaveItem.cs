using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Events;

public class SaveItem : MonoBehaviour
{
    public GameEvent[] myevent;
    //public UnityEvent action;

    private void Awake()
    {
        Load();
        Save();
    }
    public void Save()
    {
        Debug.Log(Application.persistentDataPath + "/game_Data");
        if (!Directory.Exists(Application.persistentDataPath + "/game_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_Data");
        }

        BinaryFormatter formatter = new BinaryFormatter();
        //action.Invoke();
        foreach (var _event in myevent)
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_Data/" + _event.ToString() + ".txt");
            var json = JsonUtility.ToJson(_event);
            formatter.Serialize(file, json);
            file.Close();
        }
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        foreach (var _event in myevent)
        {
            if (File.Exists(Application.persistentDataPath + "/game_Data/" + _event.ToString() + ".txt"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/game_Data/" + _event.ToString() + ".txt", FileMode.Open);

                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), _event);

                file.Close();
            }
        }
    }
}
