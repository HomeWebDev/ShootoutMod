using UnityEngine;
using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif


public class CreateUserSettingAsset
{
    #if UNITY_EDITOR
    [MenuItem("Assets/Create/UserSettings")]
    public static UserSettings Create()
    {
        UserSettings asset = ScriptableObject.CreateInstance<UserSettings>();


        AssetDatabase.CreateAsset(asset, "Assets/UserSettings.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
    #endif
}