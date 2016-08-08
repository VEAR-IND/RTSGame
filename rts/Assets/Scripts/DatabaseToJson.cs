using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class DatabaseToJson : MonoBehaviour {

    public ItemDatabase itemDatabase;
    void BeforeStart()
    {
        itemDatabase = GetComponent<ItemDatabase>();
        //itemDatabase.database = JsonUtility.FromJson<List<Item>>(File.ReadAllText(Application.dataPath + "/Resourses/Data/Items.json"));
    }
}
