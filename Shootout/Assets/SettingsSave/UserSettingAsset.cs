using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateUserSettingAsset
{
    [MenuItem("Assets/Create/UserSettings")]
    public static UserSettings Create()
    {
        UserSettings asset = ScriptableObject.CreateInstance<UserSettings>();

        AssetDatabase.CreateAsset(asset, "Assets/UserSettings.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}