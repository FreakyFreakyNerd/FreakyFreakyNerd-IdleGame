using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorPrefabs
{
    private static void CreateObject(string path, MenuCommand info)
    {
        Object t = (Object)AssetDatabase.LoadAssetAtPath("Assets/FreakyFreakyNerd/Prefabs/" + path + ".prefab", typeof(Object));
        Object test;
        if((info.context as GameObject) != null)
            test = Editor.Instantiate(t, (info.context as GameObject).transform);
        else
            test = Editor.Instantiate(t);

        test.name = test.name.Remove(test.name.Length - 7);
    }

    [MenuItem("GameObject/IdleGame/Handlers/LoggerHandler")]
    public static void LoggerHandlerSpawn(MenuCommand menuCommand)
    {
        CreateObject("Logger Handler", menuCommand);
    }

    [MenuItem("GameObject/IdleGame/UI/Theme Image")]
    public static void ThemeImageSpawn(MenuCommand menuCommand)
    {
        CreateObject("Theme Image", menuCommand);
    }

    [MenuItem("GameObject/IdleGame/UI/Theme Button")]
    public static void ThemeButtoneSpawn(MenuCommand menuCommand)
    {
        CreateObject("Theme Button", menuCommand);
    }
}
