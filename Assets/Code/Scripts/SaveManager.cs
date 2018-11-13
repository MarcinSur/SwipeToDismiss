using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;

public class MonsterData
{
}
public class SaveManager : MonoBehaviour
{
    public bool haveSavedData { get; }
    public string savePath;
    public TestData dataTest;

    public Dictionary<string, MonsterData> _monstersData;

    private void Start()
    {
       
        SaveToFile();
    }

    void LoadMonsters(Dictionary<string, MonsterData> monsterData)
    {
        _monstersData = monsterData;
        char[] separator = new char[] { '\r', '\n' };

        foreach (var monsterAssets in Resources.LoadAll<TextAsset>("Monsters/"))
        {
            List<string> monsterText = monsterAssets.text.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            int index = 2;
            while (monsterText[index] != ".end")
            {
                List<string> data = Regex.Matches(monsterText[index++].Replace("\t", " "),
                    @"[\""].+?[\""]|[^ ]+").Cast<Match>().Select(m => m.Value).ToList();
                for (int i = 0; i < data.Count; i++)
                    data[i] = data[i].Replace("\"", "").Replace("%", "");

                Debug.Log(data[0]);
            }
            index += 2;
        }

    }

    public void SaveToFile()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/chatacter_data/"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/chatacter_data");

        FileStream file = new FileStream(Application.persistentDataPath + "/game_save/chatacter_data/charatcer_save.bin", FileMode.Create);

        BinaryFormatter bf = new BinaryFormatter();
        var json = JsonUtility.ToJson(dataTest, false);
        try
        {
            bf.Serialize(file, json);
        }
        catch (System.Exception)
        {
            Debug.Log("Failed ");
            throw new System.Exception();
        }
        finally
        {
            file.Close();
            Debug.Log("Save in " + Application.persistentDataPath);
        }

    }

    public TestData LoadFromFile()
    {
        TestData td = new TestData();
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/chatacter_data/charatcer_save.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/chatacter_data/charatcer_save.txt", FileMode.Open);
            Debug.Log(file.Length);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), td);
            file.Close();
        }
        return td;
    }


}
