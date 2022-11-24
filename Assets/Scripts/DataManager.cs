using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int jelatin;
    public int gold;
    public bool[] slime_unlock_list = new bool[6];
    public List<Slime> slime_list = new List<Slime>();
}

public class DataManager : MonoBehaviour
{
    string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        JsonLoad();
    }

    public void JsonLoad()
    {
        SaveData save_data = new SaveData();

        if (!File.Exists(path))
        {
            GameManager.instance.jelatin = 0;
            GameManager.instance.gold = 0;
            JsonSave();
        }
        else
        {
            string load_json = File.ReadAllText(path);
            save_data = JsonUtility.FromJson<SaveData>(load_json);

            if (save_data != null)
            {
                for (int i = 0; i < save_data.slime_list.Count; ++i)
                    GameManager.instance.slime_data_list.Add(save_data.slime_list[i]);
                for (int i = 0; i < save_data.slime_unlock_list.Length; ++i)
                    GameManager.instance.slime_unlock_list[i] = save_data.slime_unlock_list[i];
                GameManager.instance.jelatin = save_data.jelatin;
                GameManager.instance.gold = save_data.gold;
            }
        }
    }

    public void JsonSave()
    {
        SaveData save_data = new SaveData();

        for (int i = 0; i < GameManager.instance.slime_list.Count; ++i)
        {
            SlimeAutoMove jelly = GameManager.instance.slime_list[i];
            save_data.slime_list.Add(new Slime(jelly.gameObject.transform.position, jelly.id, jelly.level));
        }
        for (int i = 0; i < GameManager.instance.slime_unlock_list.Length; ++i)
            save_data.slime_unlock_list[i] = GameManager.instance.slime_unlock_list[i];
        save_data.jelatin = GameManager.instance.jelatin;
        save_data.gold = GameManager.instance.gold;

        string json = JsonUtility.ToJson(save_data, true);

        File.WriteAllText(path, json);
    }
}
