using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    public static Collectibles savedCollectibles = new Collectibles();

    public static void Save()
    {
        savedCollectibles = Collectibles.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Path.Combine(Application.persistentDataPath, "savedCollectibles.gd"));
        bf.Serialize(fs, savedCollectibles);
        fs.Close();
    }

    public static void Load()
    {
        if(File.Exists(Path.Combine(Application.persistentDataPath, "savedCollectibles.gd")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Path.Combine(Application.persistentDataPath, "savedCollectibles.gd"), FileMode.Open);
            savedCollectibles = (Collectibles)bf.Deserialize(fs);
            Collectibles.current = savedCollectibles;
        }
    }
}
