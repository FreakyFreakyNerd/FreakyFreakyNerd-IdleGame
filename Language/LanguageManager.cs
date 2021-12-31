using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;
    private const string LanguageFolder = "language";
    private string path;

    public delegate void Function();
    public static Function OnLanguageLoad;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            path = Application.streamingAssetsPath + Path.DirectorySeparatorChar + LanguageFolder;
        }
        else
        {
            Logger.Log("LanguageManager Already Instanced Will Destroy: " + name);
            Destroy(gameObject);
        }
        LoadLanguage("en");
    }

    [SerializeField]
    private static Dictionary<string, string> localizedStrings = new Dictionary<string, string>();

    public static void LoadLanguage(string langcode)
    {
        localizedStrings = new Dictionary<string, string>();

        string file = Instance.path + Path.DirectorySeparatorChar + langcode + ".json";
        string data = "";


        if (File.Exists(file))
            data = File.ReadAllText(file);

        localizedStrings = JsonConvert.DeserializeObject<Dictionary<string,string>>(data);
        Logger.Log(JsonUtility.FromJson<Dictionary<string, string>>(data).Keys.Count);
    }

    public static string GetLocalizedText(string code)
    {
        if (localizedStrings.ContainsKey(code))
        {
            return localizedStrings[code];
        }
        return code;
    }
    public static string GetLocalizedText(string code, params object[] args)
    {
        if (localizedStrings.ContainsKey(code))
        {
            return string.Format(localizedStrings[code], args);
        }
        return code;
    }

    public static void CallOnLanguageLoad()
    {
        if(OnLanguageLoad != null)
        {
            OnLanguageLoad();
        }
    }
}
