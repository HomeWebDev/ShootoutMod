using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class UserSettings : ScriptableObject
{
    public string FilePath = "";
    public bool EnableSave = false;
    public bool EnableLoad = false;
    public Collectibles UserCollectibles;
    public SoundSettings UserSoundSettings;

    public void Save()
    {
        if (EnableSave)
        {
            FilePath = Path.Combine(Application.persistentDataPath, "UserSettings.xml");

            XMLSerializerUtility.SerializeToFile<UserSettings>(FilePath, this);
        }
    }

    public UserSettings Load()
    {
        if(EnableLoad)
        {
            FilePath = Path.Combine(Application.persistentDataPath, "UserSettings.xml");
            return XMLSerializerUtility.DeserializeFromFile<UserSettings>(FilePath);
        }
        return null;

    }


}
